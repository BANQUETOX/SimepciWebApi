using DataAccess.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Citas;
using DTO;
using DTO.Usuarios;
using DTO.EspecialidadesMedicas;
using System.Runtime.InteropServices;
using Azure.Core;
using DTO.Doctores;

namespace AppLogic
{
    public class CitaManager
    {
        CitaCrud citaCrud = new CitaCrud();
        DoctorCrud doctorCrud = new DoctorCrud();
        UsuarioCrud usuarioCrud = new UsuarioCrud();
        EspecialidadMedicaCrud especialidadMedicaCrud = new EspecialidadMedicaCrud();
        PacienteCrud pacienteCrud = new PacienteCrud();
        EmailManager emailManager = new EmailManager();


        public List<Cita> GetAllCitas()
        {
            List<Cita> citas = new List<Cita>();
            try
            {
                citas = citaCrud.GetAllCitas();  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return citas;
        }

        public string CrearCita(CitaInsert citaInsert)
        {
            string result;
            try
            {
                

                if (validarFechaCita(citaInsert.horaInicio, citaInsert.horaFinal))
                {
                    Cita cita = CastCitaInsert(citaInsert);
                    if (cita.idDoctor == 0)
                    {
                        result = "No hay doctor disponible en la sede y especialidad solicitadas";
                        return result;  
                    }


                    Cita citaExistente = new Cita();
                    citaExistente = citaCrud.GetCitaByDoctorFecha(cita.idDoctor,cita.horaInicio,cita.horaFinal);
                    if(citaExistente.Id != 0)
                    {
                        return "La cita ya ha sido agendada";
                    }

                    citaCrud.Create(cita);
                    emailManager.SendConfirmacionCita(cita);
                    result = "Cita Creada";
                }
                else
                {
                    result = "Fecha invalida";
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
         
            return result; ;
        }

        public Cita CastCitaInsert(CitaInsert citaInsert) {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(citaInsert.correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            Cita cita = new Cita();
          /*  DateTime horaInicioCr = citaInsert.horaInicio.AddHours(-6);
            DateTime horaFinalCr = citaInsert.horaFinal.AddHours(-6);*/
            Doctor doctorAsignado = GetDoctorDisponible(citaInsert.horaInicio, citaInsert.horaFinal, citaInsert.idSede, citaInsert.idEspecialidad);
            cita.idDoctor = doctorAsignado.Id;
            cita.idPaciente = paciente.Id;
            cita.idSede = citaInsert.idSede;
            cita.horaInicio = citaInsert.horaInicio;
            cita.horaFinal = citaInsert.horaFinal;
            return cita;
        }


        public List<CitaOutputReservada> CitasReservadas(int idEspecialidad, int idSede) {
            List<CitaOutputReservada> citasOutput = new List<CitaOutputReservada>();
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFinal = fechaInicio.AddDays(14);
            List<Cita> citas =  citaCrud.GetCitasReservadas(fechaInicio, fechaFinal, idEspecialidad, idSede);
            foreach (Cita cita in citas)
            {
                citasOutput.Add(castCitaReservadaOutput(cita));
            }
            return citasOutput;
            
        }
        public List<Cupo> cuposDisponibles(int idSede, int idEspecialidad)
        {
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFinal = fechaInicio.AddDays(2);
            List<Cupo> cuposDisponibles = new List<Cupo>();
            var doctores = doctorCrud.DoctoresBySedeAndEspecialidad(idSede, idEspecialidad);
            foreach (var doctor in doctores)
            {
                var cupos = cuposDiponiblesDoctor(fechaInicio, fechaFinal, doctor.Id);
                cuposDisponibles = cuposDisponibles.Concat(cupos).ToList();
            }
            return cuposDisponibles;
        }

        public List<CitaOutput> CitasPaciente(string correoPaciente)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            List<Cita> citasPaciente = citaCrud.GetCitasPaciente(paciente.Id);
            List<CitaOutput> citasOutput = new List<CitaOutput>();

            foreach (var cita in citasPaciente)
            {
                Doctor doctor = doctorCrud.GetDoctorById(cita.idDoctor);
                Usuario usuarioDoctor = usuarioCrud.RetrieveByDoctorId(doctor.Id);
                EspecialidadMedica especialidad = especialidadMedicaCrud.GetEspecialidadById(doctor.Id);
                CitaOutput citaOutput = new CitaOutput();
                citaOutput.id = cita.Id;
                citaOutput.doctor = usuarioDoctor.nombre;
                citaOutput.fecha = cita.horaInicio;
                citaOutput.especialidad = especialidad.nombre;
                citaOutput.precio = especialidad.costoCita;
                citasOutput.Add(citaOutput);
            }

            return citasOutput;
        }

        public List<CitaOutputDoctor> CitasDoctor(string correoDoctor)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(correoDoctor);
            Doctor doctor = doctorCrud.GetDoctorByUsuarioId(usuario.Id);
            List<Cita> citasDoctor = citaCrud.GetCitasDoctor(doctor.Id);
            List<CitaOutputDoctor> citasOutput = new List<CitaOutputDoctor>();
            if (citasDoctor.Count() > 0)
            {
                foreach (var cita in citasDoctor)
                {
                    CitaOutputDoctor output = castCitaOutputDoctor(cita);
                    citasOutput.Add(output);
                }
            }
            return citasOutput;
        }

        public Doctor GetDoctorDisponible(DateTime horaInicio, DateTime horaFinal, int idSede,int idEspecialidad)
        {
            Doctor doctorDisponible = new Doctor();
            DoctorCrud doctorCrud = new DoctorCrud();
            Cupo cupoSolicitado = new Cupo();
            cupoSolicitado.horaInicio = horaInicio;
            cupoSolicitado.horaFinal = horaFinal;   
            List<Doctor> doctores = doctorCrud.DoctoresBySedeAndEspecialidad(idSede, idEspecialidad);
            foreach (Doctor doctor in doctores)
            {
                List<Cupo> cuposDoctor = cuposDiponiblesDoctor(horaInicio, horaFinal, doctor.Id);
                if(cuposDoctor.Count() > 0) 
                {
                    doctorDisponible = doctor;
                }
            }

            return doctorDisponible;


        }

        public List<Cupo> cuposDiponiblesDoctor(DateTime fechaInicio,DateTime fechaFinal,int idDoctor)
        {

            var cuposDisponibles = new List<Cupo>();
            Doctor doctor = doctorCrud.GetDoctorById(idDoctor);

            if (doctor != null)
            {
                DateTime fechaActual = fechaInicio;

                while (fechaActual <= fechaFinal)
                {
                    DateTime horaInicio;
                    DateTime horaFin;

                    // Determinar el horario del doctor para la fecha actual
                    switch (doctor.horario)
                    {
                        case 1:
                            horaInicio = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 6, 0, 0);
                            horaFin = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 14, 0, 0);
                            break;
                        case 2:
                            horaInicio = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 14, 0, 0);
                            horaFin = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 22, 0, 0);
                            break;
                        case 3:
                            horaInicio = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 22, 0, 0);
                            // Para el horario nocturno (case 3), horaFin será hasta medianoche (23:59:59)
                            horaFin = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 23, 59, 59);
                            break;
                        default:
                            // Horario no válido
                            return new List<Cupo>();
                    }

                    // Generar cupos cada 30 minutos dentro del horario del doctor y para la fecha actual
                    DateTime inicio = horaInicio;
                    while (inicio < horaFin && inicio < fechaFinal)
                    {
                        DateTime siguiente = inicio.AddMinutes(30);

                        // Verificar si el siguiente cupo se encuentra dentro del rango [inicio, horaFin)
                        if (siguiente <= horaFin)
                        {
                            Cupo cupo = new Cupo
                            {
                                horaInicio = inicio,
                                horaFinal = siguiente
                            };

                            cuposDisponibles.Add(cupo);
                        }

                        inicio = siguiente;
                    }

                    // Avanzar a la siguiente fecha
                    fechaActual = fechaActual.AddDays(1);
                }

                // Obtener cupos ocupados a partir de citas agendadas
                var cuposOcupados = citaCrud.GetCitasDoctor(idDoctor)
                                             .Select(cita => new Cupo
                                             {
                                                 horaInicio = cita.horaInicio,
                                                 horaFinal = cita.horaFinal
                                             })
                                             .ToList();

                // Filtrar cupos disponibles para eliminar los cupos ocupados
                cuposDisponibles = cuposDisponibles.Where(c => !cuposOcupados.Any(co => co.horaInicio <= c.horaInicio && co.horaFinal > c.horaInicio)).ToList();
            }

            return cuposDisponibles;
        }

        public string DeleteCita(int idCita)
        {
            string result;
            try
            {
                /*Cita cita = citaCrud.GetCitaById(idCita);
                TimeSpan diferencia = cita.horaInicio - horaCancelacion;
                double horasRestantes = diferencia.TotalHours;
                if (horasRestantes < 24 )
                {
                    return "Quedan menos de 24 horas para la cita, no se puede cancelar";
                }*/
                citaCrud.DeleteCita(idCita);
                result = "Cita eliminada";
            }
            catch (Exception ex) {
                result = "No se puede eliminar la cita, debido a que tiene una factura asociada";
                Console.WriteLine(ex);    
            }
            return result;
        }

        public async Task<string> EnviarRecordatoriosCitas()
        {
            string result;
            try
            {
                List<Usuario> usuarios = usuarioCrud.RetrieveAll();
                foreach (var usuario in usuarios)
                {
                    try
                    {
                      result = await emailManager.SendRecordatorio(usuario);
                      Console.WriteLine($"{result}");
                    }
                    catch (Exception e ) { 
                        Console.WriteLine(e.Message);
                    }
                }
                return "Recordatorios enviados";
            }
            catch(Exception e)
            {
                return e.Message;            }
        }

        public bool validarFechaCita(DateTime horaInicio, DateTime horaFinal)
        {
            if (horaInicio.Date != horaFinal.Date)
            {
                return false;
            }
            TimeSpan intervalo = horaFinal - horaInicio;
            if (intervalo != TimeSpan.FromMinutes(30))
            {
                return false;
            }
            if ((horaInicio.Minute != 0 && horaInicio.Minute != 30) || (horaFinal.Minute != 0 && horaFinal.Minute != 30))
            {
                return false;
            }

            return true;
        }

        public CitaOutputReservada castCitaReservadaOutput(Cita citaBase)
        {
            Usuario usuarioPaciente = usuarioCrud.RetrieveByPacienteId(citaBase.idPaciente);
            string nombrePaciente = $"{usuarioPaciente.nombre} - {usuarioPaciente.primerApellido} - {usuarioPaciente.segundoApellido}";
            EspecialidadMedica especialidad = especialidadMedicaCrud.GetEspecialidadByCitaId(citaBase.Id);
            CitaOutputReservada citaOutputReservada = new CitaOutputReservada();
            citaOutputReservada.Id = citaBase.Id; 
            citaOutputReservada.nombrePaciente = nombrePaciente;
            citaOutputReservada.idPaciente = citaBase.idPaciente;
            citaOutputReservada.idDoctor = citaBase.idDoctor;   
            citaOutputReservada.horaInicio = citaBase.horaInicio;
            citaOutputReservada.horaFinal = citaBase.horaFinal;
            citaOutputReservada.idSede = citaBase.idSede;
            citaOutputReservada.especialidad = especialidad.nombre;
            return citaOutputReservada;

        }
        public CitaOutputDoctor castCitaOutputDoctor(Cita citaBase)
        {
            Usuario usuarioPaciente = usuarioCrud.RetrieveByPacienteId(citaBase.idPaciente);
            string nombreCompletoPaciente = $"{usuarioPaciente.nombre} - {usuarioPaciente.primerApellido} - {usuarioPaciente.segundoApellido}";
            CitaOutputDoctor citaOutputDoctor = new CitaOutputDoctor();
            citaOutputDoctor.Id = citaBase.Id;
            citaOutputDoctor.idPaciente = citaBase.idPaciente ;
            citaOutputDoctor.nombrePaciente = nombreCompletoPaciente;
            citaOutputDoctor.idDoctor = citaBase .idDoctor;
            citaOutputDoctor.horaInicio = citaBase.horaInicio;
            citaOutputDoctor.horaFinal= citaBase.horaFinal;
            citaOutputDoctor.idSede= citaBase.idSede;
            return citaOutputDoctor;
        }


    }
}

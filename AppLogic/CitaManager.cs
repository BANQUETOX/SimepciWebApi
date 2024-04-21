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

namespace AppLogic
{
    public class CitaManager
    {
        CitaCrud citaCrud = new CitaCrud();
        DoctorCrud doctorCrud = new DoctorCrud();
        UsuarioCrud usuarioCrud = new UsuarioCrud();
        EspecialidadMedicaCrud especialidadMedicaCrud = new EspecialidadMedicaCrud();
        PacienteCrud pacienteCrud = new PacienteCrud();


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
                    citaCrud.Create(cita);
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
            DateTime fechaFinal = DateTime.Now;
            fechaFinal = fechaFinal.AddDays(14);
            List<Cita> citas =  citaCrud.GetCitasReservadas(fechaInicio, fechaFinal, idEspecialidad, idSede);
            foreach (Cita cita in citas)
            {
                citasOutput.Add(castCitaReservadaOutput(cita));
            }
            return citasOutput;
            
        }
       /* public List<Cupo> cuposDisponibles( int idSede, int idEspecialidad)
        {
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFinal = fechaInicio.AddDays(1);
            List<Cupo> cuposDisponibles = new List<Cupo>();
            var doctores = doctorCrud.DoctoresBySedeAndEspecialidad(idSede,idEspecialidad);
            foreach (var doctor in doctores)
            {
                var cupos = cuposDiponiblesDoctor(fechaInicio,fechaFinal,doctor.Id);
                cuposDisponibles = cuposDisponibles.Concat(cupos).ToList();
            }
            return cuposDisponibles;
        }*/

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

        public List<Cita> CitasDoctor(int idDoctor)
        {
            List<Cita> citasDoctor = citaCrud.GetCitasDoctor(idDoctor);
            return citasDoctor;
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
                if(cuposDiponiblesDoctor(horaInicio, horaFinal, doctor.Id) != null)
                {
                    doctorDisponible = doctor;
                }
            }

            return doctorDisponible;


        }

        public List<Cupo> cuposDiponiblesDoctor(DateTime fechaInicio,DateTime fechaFinal,int idDoctor)
        {

            DateTime inicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, fechaInicio.Hour, 0, 0);
            DateTime fin = new DateTime(fechaFinal.Year, fechaFinal.Month, fechaFinal.Day, fechaFinal.Hour, 0, 0);
            var cuposTotales = new List<Cupo>();
            Doctor doctor = doctorCrud.GetDoctorById(idDoctor);
            if(doctor.horario == 1)
            {
                DateTime horaEntrada = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 6, 0, 0);
                DateTime horaSalida = new DateTime(fechaFinal.Year, fechaInicio.Month, fechaInicio.Day, 14, 0, 0);
                while (inicio < fin)
                {
                    DateTime siguiente = inicio.AddMinutes(30);
                    if (inicio >= horaEntrada && horaSalida <= siguiente)
                    {
                    Cupo cupo = new Cupo();
                    cupo.horaInicio = inicio;
                    cupo.horaFinal = siguiente;
                    cuposTotales.Add(cupo);
                    inicio = siguiente;
                    }
                }

            }
            else if (doctor.horario == 2)
            {
                DateTime horaEntrada = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 14, 0, 0);
                DateTime horaSalida = new DateTime(fechaFinal.Year, fechaInicio.Month, fechaInicio.Day, 22, 0, 0);
                while (inicio < fin)
                {
                    DateTime siguiente = inicio.AddMinutes(30);
                    if (inicio >= horaEntrada && horaSalida <= siguiente)
                    {
                        Cupo cupo = new Cupo();
                        cupo.horaInicio = inicio;
                        cupo.horaFinal = siguiente;
                        cuposTotales.Add(cupo);
                        inicio = siguiente;
                    }
                }

            }
            else if (doctor.horario == 3)
            {
                DateTime horaEntrada = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 22, 0, 0);
                DateTime horaSalida = new DateTime(fechaFinal.Year, fechaInicio.Month, fechaInicio.Day, 6, 0, 0);
                while (inicio < fin)
                {
                    DateTime siguiente = inicio.AddMinutes(30);
                    if (inicio >= horaEntrada && horaSalida <= siguiente)
                    {
                        Cupo cupo = new Cupo();
                        cupo.horaInicio = inicio;
                        cupo.horaFinal = siguiente;
                        cuposTotales.Add(cupo);
                        inicio = siguiente;
                    }
                }
            }

            var cuposOcupados = new List<Cupo>();
            var citasAgendadas = citaCrud.GetCitasDoctor(idDoctor);
            foreach (var cita in citasAgendadas)
            {
                Cupo cupoCita = new Cupo();
                cupoCita.horaInicio = cita.horaInicio;
                cupoCita.horaFinal = cita.horaFinal;
                cuposOcupados.Add(cupoCita); 
            }

            var cuposDisponibles = new List<Cupo>();
            cuposDisponibles = cuposTotales.Except(cuposOcupados).ToList();
            return cuposDisponibles;
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
            EspecialidadMedica especialidad = especialidadMedicaCrud.GetEspecialidadByCitaId(citaBase.Id);
            CitaOutputReservada citaOutputReservada = new CitaOutputReservada();
            citaOutputReservada.Id = citaBase.Id;   
            citaOutputReservada.idPaciente = citaBase.idPaciente;
            citaOutputReservada.idDoctor = citaBase.idDoctor;   
            citaOutputReservada.horaInicio = citaBase.horaInicio;
            citaOutputReservada.horaFinal = citaBase.horaFinal;
            citaOutputReservada.idSede = citaBase.idSede;
            citaOutputReservada.especialidad = especialidad.nombre;
            return citaOutputReservada;

        }
    }
}

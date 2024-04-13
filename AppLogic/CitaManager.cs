using DataAccess.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Citas;
using DTO;

namespace AppLogic
{
    public class CitaManager
    {
        CitaCrud citaCrud = new CitaCrud();
        DoctorCrud doctorCrud = new DoctorCrud();


        public string CrearCita(CitaInsert citaInsert)
        {
            string result;
            if (validarFechaCita(citaInsert.horaInicio, citaInsert.horaFinal))
            {
            Cita cita = CastCitaInsert(citaInsert);
            citaCrud.Create(cita);
                result = "Cita Creada";
            }
            else
            {
                result = "Fecha invalida";
            }
            return result; ;
        }

        public Cita CastCitaInsert(CitaInsert citaInsert) {
            Cita cita = new Cita();
            Doctor doctorAsignado = GetDoctorDisponible(citaInsert.horaInicio, citaInsert.horaFinal, citaInsert.idSede, citaInsert.idEspecialidad );
            cita.idDoctor = doctorAsignado.Id;
            cita.idPaciente = citaInsert.idPaciente;
            cita.idSede = citaInsert.idSede;
            cita.horaInicio = citaInsert.horaInicio;
            cita.horaFinal = citaInsert.horaFinal;
            return cita;
        }


        public List<Cita> CitasReservadas(int idEspecialidad, int idSede) {
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFinal = DateTime.Now;
            fechaFinal = fechaFinal.AddDays(10);
            return citaCrud.GetCitasReservadas(fechaInicio, fechaFinal, idEspecialidad, idSede);
        }
        public List<Cupo> cuposDisponibles( int idSede, int idEspecialidad)
        {
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFinal = fechaInicio.AddDays(10);
            List<Cupo> cuposDisponibles = new List<Cupo>();
            var doctores = doctorCrud.DoctoresBySedeAndEspecialidad(idSede,idEspecialidad);
            foreach (var doctor in doctores)
            {
                var cupos = cuposDiponiblesDoctor(fechaInicio,fechaFinal,doctor.Id);
                cuposDisponibles = cuposDisponibles.Concat(cupos).ToList();
            }
            return cuposDisponibles;
        }

        public List<Cita> CitasPaciente(int idPaciente)
        {
            List<Cita> citasPaciente = citaCrud.GetCitasPaciente(idPaciente);
            return citasPaciente;
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
    }
}

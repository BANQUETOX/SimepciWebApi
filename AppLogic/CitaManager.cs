using DataAccess.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Citas;

namespace AppLogic
{
    public class CitaManager
    {
        CitaCrud citaCrud;
        public CitaManager() {
            citaCrud = new CitaCrud();
        }


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
            cita.idDoctor = citaInsert.idDoctor;
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
        public List<Cita> CitasDisponibles() { 
            List<Cita> citasDisponibles = new List<Cita>(); 
            return citasDisponibles;
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

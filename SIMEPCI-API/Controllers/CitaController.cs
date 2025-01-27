﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using DTO.Citas;
using DTO;
using Microsoft.AspNetCore.Cors;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        CitaManager citaManager = new CitaManager();

        [HttpPost]
        public string CrearCita(CitaInsert cita)
        {
            string result;
            try
            {

                result = citaManager.CrearCita(cita);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;  
        }

        [HttpGet]
        public List<Cita> GetAllCitas() { 
            return citaManager.GetAllCitas();
        }

        [HttpGet]
        public List<CitaOutputReservada> GetCitasReservadas(int idEspecialidad, int idSede)
        {
            List<CitaOutputReservada> result;
            try
            {
                result = citaManager.CitasReservadas(idEspecialidad,idSede);
            }
            catch (Exception ex)
            {
                result = new List<CitaOutputReservada>();
            }
            return result;  

        }

        [HttpGet]
        public List<Cupo> GetCuposDisponibles(int idSede, int idEspecialidad)
        {
            return citaManager.cuposDisponibles(idSede, idEspecialidad);
        }

        [HttpGet]
        public List<CitaOutput> GetCitasPaciente(string correoPaciente)
        {
            return citaManager.CitasPaciente(correoPaciente);
        }
        [HttpGet]
        public List<CitaOutputDoctor> GetCitasDoctor(string correoDoctor)
        {
            return citaManager.CitasDoctor(correoDoctor);
        }

        [HttpGet]
        public List<Cupo> GetCuposDisponiblesDoctor(DateTime fechaInico, DateTime fechaFinal, int idDoctor)
        {
            return citaManager.cuposDiponiblesDoctor(fechaInico, fechaFinal, idDoctor);
        }

        [HttpPost]
        public Task<string> EnviarRecordatoriosCita()
        {
            return citaManager.EnviarRecordatoriosCitas();
        }

        [HttpDelete]
        public string CancelarCita(int idCita)
        {
            return citaManager.DeleteCita(idCita);
        }
    }
}

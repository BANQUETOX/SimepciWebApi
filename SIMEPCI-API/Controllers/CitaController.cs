using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using DTO.Citas;
using DTO;

namespace SIMEPCI_API.Controllers
{
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
        public List<Cita> GetCitasReservadas(int idEspecialidad, int idSede)
        {
            List<Cita> result;
            try
            {
                result = citaManager.CitasReservadas(idEspecialidad,idSede);
            }
            catch (Exception ex)
            {
                result = new List<Cita>();
            }
            return result;  

        }

        [HttpGet]
        public List<Cupo> GetCuposDisponibles (DateTime fechaInicio, DateTime fechaFinal, int idSede, int idEspecialidad)
        {
            return citaManager.cuposDisponibles(fechaInicio,fechaFinal,idSede,idEspecialidad);
        }
    }
}

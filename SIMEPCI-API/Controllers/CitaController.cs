using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using DTO.Citas;

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
            return citaManager.CrearCita(cita);
        }

        [HttpGet]
        public List<Cita> GetCitasReservadas(int idEspecialidad, int idSede)
        {
            return citaManager.CitasReservadas(idEspecialidad,idSede);

        }
    }
}

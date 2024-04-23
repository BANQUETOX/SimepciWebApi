using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        DoctorManager manager = new DoctorManager();

        [HttpGet]

        public List<Doctor> GetAllDoctores()
        {
            return manager.GetAllDoctors();
        }
        [HttpPatch]
        public string CambiarHorarioDoctror(int idDoctor, int horario)
        {
            return manager.UpdateHorarioDoctor(idDoctor, horario);
        }
    }
}

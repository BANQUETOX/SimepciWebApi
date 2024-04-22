using AppLogic;
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

        [HttpPatch]
        public string CambiarHorarioDoctror(int idDoctor, int horario)
        {
            return manager.UpdateHorarioDoctor(idDoctor, horario);
        }
    }
}

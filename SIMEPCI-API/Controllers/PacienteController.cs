using AppLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        PacienteManager manager = new PacienteManager();

        [HttpPost]
        public void CrearPaciente(int idUsuario)
        {
            manager.CrearPaciente(idUsuario);
        }

        [HttpPost]
        public void EliminarPaciente(int idUsuario)
        {
            manager.EliminarPaciente(idUsuario);
        }
    }
}

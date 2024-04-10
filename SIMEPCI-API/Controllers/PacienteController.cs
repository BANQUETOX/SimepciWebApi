using AppLogic;
using Azure.Core;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        PacienteManager manager = new PacienteManager();

        [HttpGet]
        public List<Paciente> GetAllPacientes()
        {
           return manager.GetAllPacientes();
        }
    }
}

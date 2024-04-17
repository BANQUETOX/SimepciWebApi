using AppLogic;
using Azure.Core;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
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

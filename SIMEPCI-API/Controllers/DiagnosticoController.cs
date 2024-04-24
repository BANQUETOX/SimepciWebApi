using AppLogic;
using DTO.Diagnosticos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiagnosticoController : ControllerBase
    {
        DiagnosticoManager diagnosticoManager = new DiagnosticoManager();

        [HttpGet]
        public List<Diagnostico> GetDiagnosticosPaciente(string correoPaciente)
        {
            return diagnosticoManager.GetDiagnosticoPaciente(correoPaciente);
        }

        [HttpPost]
        public string CrearDiagnostico (DiagnosticoInput diagnostico)
        {
            return diagnosticoManager.CreateDiagnostico(diagnostico);
        }
    }
}

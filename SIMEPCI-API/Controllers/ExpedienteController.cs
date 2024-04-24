using AppLogic;
using DTO.Expedientes;
using DTO.InfosExpediente;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpedienteController : ControllerBase
    {
        ExpedienteManager manager = new ExpedienteManager();
        InfoExpedienteManager infoManager = new InfoExpedienteManager();   

        [HttpPost]
        public string adjuntarNotaMedica(InfoExpedienteInput infoExpediente)
        {
            return infoManager.CreateNotaMedica(infoExpediente);
        }

        [HttpPost]
        public string adjuntarNotaEnfermeria(InfoExpedienteInput infoExpediente)
        {
            return infoManager.CreateNotaEnfermeria(infoExpediente);
        }
        [HttpPost]
        public string adjuntarHistorialMedico(InfoExpedienteInput infoExpediente)
        {
            return infoManager.CreateHistorialMedico(infoExpediente);
        }

        [HttpGet]
        public ExpedienteCompleto ExpedienteCompletoPaciente(string correoPaciente)
        {
            ExpedienteCompleto expedietente = new ExpedienteCompleto();
            try
            {
                expedietente = manager.GetExpedienteCompleto(correoPaciente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return expedietente;
            }
            return expedietente;
        }
    }
}

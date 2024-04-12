using AppLogic;
using DTO.Expedientes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpedienteController : ControllerBase
    {
        ExpedienteManager manager = new ExpedienteManager();

        [HttpPost]
        public string CrearExpediente(ExpedienteInput expediente)
        {
            string result;
            try
            {

               result = manager.CreateExpediente(expediente);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        [HttpGet]
        public ExpedienteCompleto ExpedienteCompletoPaciente(int idPaciente)
        {
            ExpedienteCompleto expedietente = new ExpedienteCompleto();
            try
            {
                expedietente = manager.GetExpedienteCompleto(idPaciente);
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

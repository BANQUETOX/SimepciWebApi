using AppLogic;
using DTO.Expedientes;
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

        [HttpPatch]
        public string CrearExpediente(ExpedienteInput expediente)
        {
            string result;
            try
            {

                result = manager.InicializarExpediente(expediente);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
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

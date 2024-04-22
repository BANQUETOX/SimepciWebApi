using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfiguracionController : ControllerBase
    {
        ConfiguracionManager manager = new ConfiguracionManager();


        [HttpGet]
        public List<Configuracion> GetAllConfiguraciones()
        {
            return manager.GetConfiguraciones();
        }

        [HttpPost]
        public string ActualizarImpuesto(string valorImpuesto)
        {
            return manager.UpdateImpuesto(valorImpuesto);
        }

        [HttpPost]
        public string ActualizarRecordatorio(string valorRecordatorio)
        {
            return manager.UpdateRecordatorio(valorRecordatorio);
        }

        [HttpPost]
        public string ActualizarIVA(string valorRecordatorio)
        {
            return manager.UpdateIva(valorRecordatorio);
        }
    }
}

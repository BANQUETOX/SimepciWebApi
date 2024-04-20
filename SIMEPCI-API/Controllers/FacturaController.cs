using AppLogic;
using DTO.Facturas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        FacturaManager manager = new FacturaManager();

        [HttpPost]
        public string CrearFactura(FacturaInput input)
        {
            return manager.CrearFactura(input);
        }

        [HttpGet]
        public List<FacturaCompleta> GetFacturasPaciente(string correoPaciente)
        {
            return manager.GetFacturasPaciente(correoPaciente);
        }
    }
}

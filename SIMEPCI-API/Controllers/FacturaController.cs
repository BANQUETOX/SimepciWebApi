using AppLogic;
using DTO;
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

        [HttpGet]
        public List<ReporteMensual> GetReportesFinancieros()
        {
            return manager.reporteGanancias();
        }

        [HttpPatch]
        public Task<string> PagarFactura(int idFactura)
        {
            return manager.UpdateFacturaPagada(idFactura);
        }

        [HttpPatch]
        public string SetFacturaSinPagar(int idFactura)
        {
            return manager.UpdateFacturaSinPagar(idFactura);
        }
    }
}

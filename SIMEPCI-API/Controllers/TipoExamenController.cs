using AppLogic;
using Azure.Core;
using DTO.TiposExamenes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TipoExamenController : ControllerBase
    {

        TipoExamenManager manager = new TipoExamenManager();

        [HttpGet]

        public List<TipoExamen> GetAllTiposExamenes()
        {
            return manager.GetTiposExamenes();
        }

        [HttpPost]
        public string CrearTipoExamen(TipoExamenInsert tipoExamenInsert)
        {
            return manager.CrearTipoExamen(tipoExamenInsert);
        }

        [HttpPatch]

        public string AcualizarTipoExamen(TipoExamen tipoExamen)
        {
            return manager.ActualizarTipoExamen(tipoExamen);
        }

        [HttpDelete]
        public string EliminarTipoExamen(int idTipoExamen)
        {
            return manager.EliminarTipoExamen(idTipoExamen);
        }
    }
}

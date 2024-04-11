using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        RecetaManager manager = new RecetaManager();

        [HttpPost]
        public void CreateReceta(RecetaInput recetaInput)
        {
            manager.CrearReceta(recetaInput);
        }
        [HttpGet]
        public List<Receta> GetRecetasPaciente(int idPaciente)
        {
            return manager.GetRecetasPaciente(idPaciente);
        }
    }
}

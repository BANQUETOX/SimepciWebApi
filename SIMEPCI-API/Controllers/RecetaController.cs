using AppLogic;
using DTO.Recetas;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        RecetaManager manager = new RecetaManager();

        [HttpPost]
        public string CreateReceta(RecetaInput recetaInput)
        {
            string result;
            try
            {

              result = manager.CrearReceta(recetaInput);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        [HttpGet]
        public List<RecetaOutput> GetRecetasPaciente(string correoPaciente)
        {
            List<RecetaOutput> result;
            try
            {

             result = manager.GetRecetasPaciente(correoPaciente);
            }
            catch (Exception ex)
            {
                result = new List<RecetaOutput>();
            }
            return result;
        }

        [HttpPatch]
        public string UpdateReceta(Receta receta) { 
            return manager.UpdateReceta(receta);    
        }

        [HttpDelete]
        public string DeleteReceta(int idReceta)
        {
            return manager.DeleteReceta(idReceta);
        }
    }
}

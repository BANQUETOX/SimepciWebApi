using AppLogic;
using DTO.ExamenesMedicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExamenMedicoController : ControllerBase
    {
        ExamenMedicoManager manager = new ExamenMedicoManager();

        [HttpPost]
        public string CrearExamenMedico(ExamenMedicoInsert examenMedico)
        {
            string result;
            try
            {

              result = manager.CrearExamenMedico(examenMedico);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        [HttpGet]
        public List<ExamenMedico> GetExamenMedicosPaciente(int idUsuario)
        {
            return manager.GetExamenMedicosPaciente(idUsuario);
        }

        [HttpGet]
        public ExamenMedico GetExamenMedicoById(int idExamenMedico) {
            return manager.GetExamenMedicoById(idExamenMedico);
        }

        [HttpPatch]
        public string SubirResultado(string resultado, int idExamenMedico)
        {
            return manager.UpdateExamenMedico(resultado, idExamenMedico);
        }

        [HttpDelete]
        public string EliminarExamenMedico(int idExamenMedico)
        {
            return manager.DeleteExamenMedico(idExamenMedico);
        }
    }
}

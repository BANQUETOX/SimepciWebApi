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
        public void CrearExamenMedico(ExamenMedicoInsert examenMedico)
        {
            manager.CrearExamenMedico(examenMedico);
        }

        [HttpGet]
        public List<ExamenMedico> GetExamenMedicosPaciente(int idPaciente)
        {
            return manager.GetExamenMedicosPaciente(idPaciente);
        }
    }
}

using AppLogic;
using DTO;
using DTO.ExamenesMedicos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
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
        public List<ExamenMedicoOutput> GetExamenesMedicosPaciente(int idUsuario)
        {
            return manager.GetExamenMedicosPaciente(idUsuario);
        }

        [HttpGet]
        public List<ExamenMedicoOutput> GetExamenesMedicosByCorreoPaciente(string correoPaciente)
        {
            Paciente paciente = manager.GetPacienteByCorreo(correoPaciente);
            Console.WriteLine(paciente.Id);
            return manager.GetExamenMedicosPaciente(paciente.IdUsuario);
        }

        [HttpGet]
        public ExamenMedicoOutput GetExamenMedicoById(int idExamenMedico) {
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

using AppLogic;
using DTO.EspecialidadesMedicas;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EspecialidadMedicaController : ControllerBase
    {
        EspecialidadMedicaManager manager = new EspecialidadMedicaManager();


        [HttpGet]
        public EspecialidadMedica GetEspecialidadById(int idEspecialidadMedica)
        {
            return manager.GetEspecialidadById(idEspecialidadMedica);
        }

        [HttpGet] 
        public List<EspecialidadMedica> GetAllEspecialidadesMedicas()
        {
            return manager.GetAllEspecialidadMedicas();
        }

        [HttpPost]
        public string CrearEspecialidadMedica(EspecialidadMedicaInsert especialidadMedicaInsert)
        {
            return manager.CreateEspecialidad(especialidadMedicaInsert);
        }

        [HttpPatch]
        public string ActualizarPrecioEspecialidad(int idEspecialidad, float nuevoPrecio)
        {
            return manager.UpdatePrecioEspecialidad(idEspecialidad, nuevoPrecio);
        }
    }
}

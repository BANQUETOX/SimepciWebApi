using DataAccess.Crud;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using AppLogic;
using Azure.Core;
using DTO.Sedes;
namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SedeController : ControllerBase
    {

        SedeManager sedeManager = new SedeManager();
        [HttpGet]
        public List<Sede> GetAllSedes()
        {
            List<Sede> sedes = new List<Sede>();
            try
            {
            sedes = sedeManager.GetAllSedes();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sedes;
        }

        [HttpGet] 
        public Sede GetSedeById (int idSede) { 
            return sedeManager.GetSedeById(idSede);
        }
        [HttpPost]
        public string CrearSede(SedeInsert sede)
        {
            string result;
            try
            {

              result = sedeManager.CrearSede(sede);
            }
            catch (Exception ex)
            {
                result=ex.Message;
            }
            return result;
        }

        [HttpPatch]
        public string ActualizarSede(Sede sede)
        {
            return sedeManager.UpdateSede(sede);
        }

        [HttpDelete]
        public string EliminarSede(int idSede)
        {
            return sedeManager.DeleteSede(idSede);
        }


    }
}

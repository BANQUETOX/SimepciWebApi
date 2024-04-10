using DataAccess.Crud;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using DTO;
using AppLogic;
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

        [HttpPost]
        public string CrearSede(Sede sede)
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

    }
}

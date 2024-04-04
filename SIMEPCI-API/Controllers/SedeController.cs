using DataAccess.Crud;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Microsoft.AspNetCore.Cors;
namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SedeController : ControllerBase
    {

        SedeCrud sedeCrud = new SedeCrud();
        [HttpGet]
        public List<Sede> GetAllSedes()
        {
            List<Sede> sedes = new List<Sede>();
            try
            {
            sedes = sedeCrud.RetrieveAll<Sede>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sedes;
        }

    }
}

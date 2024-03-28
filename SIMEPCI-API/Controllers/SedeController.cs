using DataAccess.Crud;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SedeController : ControllerBase
    {

        SedeCrud sedeCrud = new SedeCrud();
        [HttpGet]
        public List<Sede> GetAllSedes()
        {
            List<Sede> sedes = new List<Sede>();
            sedes = sedeCrud.RetrieveAll<Sede>();
            return sedes;
        }

    }
}

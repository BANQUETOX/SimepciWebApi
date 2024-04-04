using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using AppLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;


namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        [HttpGet]
        public List<Rol> GetAllRols()
        {
            List<Rol> result = new List<Rol>();
            RolManager rolManager = new RolManager();

            try
            {
                result = rolManager.GetAllRols(); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
            return result;
        }
      
    }
}

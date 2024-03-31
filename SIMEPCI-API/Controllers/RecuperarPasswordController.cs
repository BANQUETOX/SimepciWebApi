using AppLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecuperarPasswordController : ControllerBase
    {
        EmailManager emailManager = new EmailManager();
        [HttpPost]
        public string CrearRecuperarPasswordOtp(string correo)
        {
            return emailManager.SendPasswordOtp(correo).GetAwaiter().GetResult();

        }
    }
}

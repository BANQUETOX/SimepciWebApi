using AppLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecuperarPasswordOtpController : ControllerBase
    {
        EmailManager emailManager = new EmailManager();
        RecuperarPasswordOtpManager otpManager = new RecuperarPasswordOtpManager();
        [HttpPost]
        public string CrearRecuperarPasswordOtp(string correo)
        {
            return emailManager.SendPasswordOtp(correo).GetAwaiter().GetResult();

        }

        [HttpGet]
        public bool ValidarPasswordOtp(string correo, string otpInput)
        {
            return otpManager.ValidarPasswordOtp(correo,otpInput);

        }
    }
}

using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistroOtpController : ControllerBase
    {
        public RegistroOtpManager registroOtpManager = new RegistroOtpManager();

        [HttpGet]
        public bool ValidarOtp(string correoUsuario,string otpInput)
        {
            return registroOtpManager.ValidarOtp(correoUsuario, otpInput);
        }

        [HttpPost]
        public string CrearRegistroOtp(string correoUsuario)
        {
            EmailManager emailManager = new EmailManager();
            string emailStatus = emailManager.SendOtp(correoUsuario).GetAwaiter().GetResult();
            return emailStatus;
        }
    }
}

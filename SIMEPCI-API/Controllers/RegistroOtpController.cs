using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
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
            string emailStatus = "";
            EmailManager emailManager = new EmailManager();
            try
            {
             emailStatus = emailManager.SendOtp(correoUsuario).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return emailStatus;
        }
    }
}

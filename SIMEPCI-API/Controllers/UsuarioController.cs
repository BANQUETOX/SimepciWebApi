using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using Microsoft.AspNetCore.Cors;
using System.Data.SqlTypes;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuarioManager usuarioManager;
        public UsuarioController() {
            usuarioManager = new UsuarioManager();
        }
        [HttpGet]
        public List<Usuario> GetAllUsers() {
            List<Usuario> result = new List<Usuario>();

            try
            {
                result = usuarioManager.GetAllUsuarios();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        [HttpPost]

        public string CreateUsuario(Usuario usuario)
        {
            string result = "";
            try
            {
                result = usuarioManager.CreateUsuario(usuario);
            }
            catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
            return result;

        }

        [HttpGet]
        public Usuario Login(string correo, string password)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            return usuarioManager.Login(correo,password);

        }

        [HttpPost]
        public void ActualizarPassword(string correoUsuario, string newpassword)
        {
            UsuarioManager usuarioManager=new UsuarioManager();
            usuarioManager.actualizarPassword(correoUsuario, newpassword);
        }
    }
}

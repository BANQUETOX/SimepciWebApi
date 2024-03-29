using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public List<Usuario> GetAllUsers() {
            List<Usuario> result = new List<Usuario>();

            try
            {
                UsuarioManager usuarioManager = new UsuarioManager();
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
            UsuarioManager usuarioManager = new UsuarioManager();
            return usuarioManager.CreateUsuario(usuario);

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

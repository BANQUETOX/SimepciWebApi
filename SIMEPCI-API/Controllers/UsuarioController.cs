using AppLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using Microsoft.AspNetCore.Cors;
using System.Data.SqlTypes;
using DTO.Usuarios;

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
        public List<UsuarioGet> GetAllUsers() {
            List<UsuarioGet> result = new List<UsuarioGet>();

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

        public string CreateUsuario(UsuarioInsert usuarioInsert)
        {
            Usuario usuario = usuarioManager.castUsuarioInsert(usuarioInsert);

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
          Usuario usuario = new Usuario();
            try
            {
                usuario = usuarioManager.Login(correo, password);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return usuario;

        }

        [HttpPost]
        public void ActualizarPassword(string correoUsuario, string newpassword)
        {
            try
            {
            usuarioManager.actualizarPassword(correoUsuario, newpassword);
            }
            catch(Exception ex )
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

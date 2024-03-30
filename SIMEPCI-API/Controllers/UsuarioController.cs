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
            return usuarioManager.CreateUsuario(usuario);

        }
    }
}

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

        [HttpGet]
        public UsuarioGet Login(string correo, string password)
        {
            UsuarioGet usuario = new UsuarioGet();
            try
            {
                usuario = usuarioManager.Login(correo, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return usuario;

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
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            return result;

        }

      

        




        [HttpPatch]
        public string ActualizarPassword(string correoUsuario, string newpassword)
        {
            string result;
            try
            {
                result = usuarioManager.actualizarPassword(correoUsuario, newpassword);
            }
            catch (Exception ex)
            {
               result=ex.ToString();    
            }
            return result;
        }

      
 

        [HttpPatch]
        public string DesactivarUsuario(string correoUsuario)
        {
            string resultado;
            try
            {
              resultado =  usuarioManager.desactivarUsuario(correoUsuario);
            }
            catch (Exception ex)
            {
                resultado =ex.ToString();
            }
            return resultado;
        }

        [HttpPatch]
        public string ActivarUsuario(string correoUsuario)
        {
            string resultado;
            try
            {
                resultado = usuarioManager.activarUsuario(correoUsuario);
            }
            catch (Exception ex)
            {
                resultado = ex.ToString();
            }
            return resultado;
        }

        


    }
}

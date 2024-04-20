using AppLogic;
using DTO.Comentarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        ComentarioManager manager = new ComentarioManager();


        [HttpPost]
        public string crearComentario(ComentarioInsert comentario)
        {
            return manager.crearComentario(comentario);
        }

        [HttpGet]
        public List<Comentario> getComentarios()
        {
            return manager.getComentarios();
        }
    }
}

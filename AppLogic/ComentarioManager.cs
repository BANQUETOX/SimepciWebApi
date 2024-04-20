using DataAccess.Crud;
using DTO.Comentarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class ComentarioManager
    {
        ComentarioCrud comentarioCrud = new ComentarioCrud();

        public string crearComentario(ComentarioInsert comentario)
        {
            string result;
            try
            {
                comentarioCrud.Create(castComentarioInsert(comentario));
                result = "Comentario enviado";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public List<Comentario> getComentarios()
        {
            List <Comentario> comentarios = new List<Comentario>();
            try
            {
                comentarios = comentarioCrud.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comentarios;
        }

        public Comentario castComentarioInsert(ComentarioInsert insert)
        {
            Comentario comentario = new Comentario();
            comentario.satisfaccion = insert.satisfaccion;  
            comentario.profecionalismo = insert.profecionalismo;
            comentario.instalaciones = insert.instalaciones;
            comentario.recomendaria = insert.recomendaria;
            comentario.comentarios = insert.comentarios;
            return comentario;
        }
    }
}

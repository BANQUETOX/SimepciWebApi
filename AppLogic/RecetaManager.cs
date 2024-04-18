using DataAccess.Crud;
using DTO;
using DTO.Recetas;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class RecetaManager
    {
        RecetaCrud crud = new RecetaCrud();
        PacienteCrud pacienteCrud = new PacienteCrud();
        UsuarioCrud usuarioCrud = new UsuarioCrud();

        public string CrearReceta(RecetaInput recetaInput)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(recetaInput.correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id); 
            Receta receta = new Receta();
            receta.idPaciente = paciente.Id;
            receta.imagen = recetaInput.imagen;
            receta.fechaEmision = DateTime.Now;
            receta.medicamento = recetaInput.medicamento;   
            receta.dosis = recetaInput.dosis;
            receta.diasDosis = recetaInput.diasDosis;
            receta.recomendaciones = recetaInput.recomendaciones;
            crud.Create(receta);
            return "Receta Creada";
        }

        public List<Receta> GetRecetasPaciente(int idUsuario) {
            return crud.GetRecetasPaciente(idUsuario);
        }

        public string UpdateReceta(Receta receta)
        {
            string result;
            try
            {
                crud.Update(receta);
                result = "Receta Actualizada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string DeleteReceta(int idReceta)
        {
            string result;
            try
            {
                crud.Delete(idReceta);
                result = "Receta Eliminada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public Receta GetRecetaById (int idReceta)
        {
            Receta receta = new Receta();
            try
            {

             receta = crud.GetRecetaById(idReceta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return receta;

        }
    }
}

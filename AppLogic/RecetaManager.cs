using DataAccess.Crud;
using DTO.Recetas;
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

        public string CrearReceta(RecetaInput recetaInput)
        {
            Receta receta = new Receta();
            receta.idPaciente = recetaInput.idPaciente;
            receta.imagen = recetaInput.imagen;
            receta.fechaEmision = recetaInput.fechaEmision;
            receta.medicamento = recetaInput.medicamento;   
            receta.dosis = recetaInput.dosis;
            receta.diasDosis = recetaInput.diasDosis;
            receta.recomendaciones = recetaInput.recomendaciones;
            crud.Create(receta);
            return "Receta Creada";
        }

        public List<Receta> GetRecetasPaciente(int idPaciete) {
            return crud.GetRecetasPaciente(idPaciete);
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

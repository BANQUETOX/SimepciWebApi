using DataAccess.Crud;
using DTO;
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
    }
}

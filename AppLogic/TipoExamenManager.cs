using Azure.Core;
using DataAccess.Crud;
using DTO.TiposExamenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class TipoExamenManager
    {
        TipoExamenCrud tipoExamenCrud = new TipoExamenCrud();  
        

        public string CrearTipoExamen(TipoExamenInsert tipoExamenInsert)
        {
            string result;
            try
            {
                TipoExamen tipoExamen = CastTipoExamenInsert(tipoExamenInsert);
                tipoExamenCrud.Create(tipoExamen);
                result = "Examen creado";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public List<TipoExamen> GetTiposExamenes()
        {
            return tipoExamenCrud.GetTiposExamenes();
        }

        public string ActualizarTipoExamen(TipoExamen tipoExamen)
        {
            string result;
            try
            {

                tipoExamenCrud.Update(tipoExamen);
                result = "Tipo Examen acualizado";
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result ;
        }

        public string EliminarTipoExamen(int idTipoExamen)
        {
            string result;
            try
            {

                tipoExamenCrud.Delete(idTipoExamen);
                result = "Tipo de examen eliminado";
            }
            catch (Exception e) { 
                result = e.Message;
            }
            return result;
        }

        public TipoExamen CastTipoExamenInsert(TipoExamenInsert tipoExamenInsert)
        {
            TipoExamen tipoExamen = new TipoExamen();
            tipoExamen.nombre = tipoExamenInsert.nombre;
            return tipoExamen;
        }
    }
}

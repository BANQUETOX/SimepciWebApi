using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class ConfiguracionManager
    {
        ConfiguracionCrud configuracionCrud = new ConfiguracionCrud();


        public List<Configuracion> GetConfiguraciones()
        {
            return configuracionCrud.GetConfuguraciones();
        }

        public string UpdateImpuesto(string nuevoValor)
        {
            string result;
            try
            {
                configuracionCrud.UpdateImpuesto(nuevoValor);
                result = "Impuesto actualizado";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string UpdateRecordatorio(string nuevoValor)
        {
            string result;
            try
            {
                configuracionCrud.UpdateRecordatorio(nuevoValor);
                result = "Recordatorios actualizados";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}

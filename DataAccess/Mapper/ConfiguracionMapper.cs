using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class ConfiguracionMapper
    {

        public Configuracion BuildObject(Dictionary<string, object> row)
        {
            Configuracion configuracion = new Configuracion();
            configuracion.Id = int.Parse(row["Id"].ToString());
            configuracion.nombre = row["Nombre"].ToString();
            configuracion.valor = row["Valor"].ToString();
            return configuracion;

        }
        public List<Configuracion> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Configuracion> results = new List<Configuracion>();

            foreach (var row in rowList)
            {
                var configuracion = BuildObject(row);
                results.Add(configuracion);
            }

            return results;
        }


        public SqlOperation GetConfiguraciones()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_CONFIGURACIONES";
            return operation;
        }

        public SqlOperation GetUpdateImpuestoStatement(string nuevoValor)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_IMPUESTO";
            operation.AddVarcharParam("nuevoValor",nuevoValor);
            return operation;
        }

        public SqlOperation GetUpdateIvaStatement(string nuevoValor)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_IVA";
            operation.AddVarcharParam("nuevoValor", nuevoValor);
            return operation;
        }


        public SqlOperation GetUpdateRecordatorioStatement(string nuevoValor)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_RECORDATORIO";
            operation.AddVarcharParam("nuevoValor", nuevoValor);
            return operation;
        }
    }
}

using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO.TiposExamenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class TipoExamenMapper 
    {
        public TipoExamen BuildObject(Dictionary<string, object> row)
        {
            TipoExamen tipoExamen = new TipoExamen();
            tipoExamen.Id = int.Parse(row["Id"].ToString());
            tipoExamen.nombre = row["Nombre"].ToString();
            return tipoExamen;
        }

        public List<TipoExamen> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<TipoExamen> results = new List<TipoExamen>();

            foreach (var row in rowList)
            {
                var tipoExamen = BuildObject(row);
                results.Add(tipoExamen);
            }

            return results;
        }

        public SqlOperation GetRetrieveAllOperation()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_TIPOS_EXAMENES";
            return operation;
        }

        public SqlOperation GetCreateStatement(TipoExamen tipoExamen)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_TIPO_EXAMEN";
            operation.AddVarcharParam("nombre",tipoExamen.nombre);
            return operation;
        }

        public SqlOperation GetDeleteStatement(int idTipoExamen)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_TIPO_EXAMEN";
            operation.AddIntegerParam("idTipoExamen", idTipoExamen);
            return operation;
        }

        public SqlOperation GetUpdateStatement(TipoExamen tipoExamen)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_TIPO_EXAMEN";
            operation.AddIntegerParam("idTipoExamen",tipoExamen.Id);
            operation.AddVarcharParam("nombre", tipoExamen.nombre);
            return operation;
        }


    }
}

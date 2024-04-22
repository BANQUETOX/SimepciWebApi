using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class EnfermeroMapper
    {
        public Enfermero BuildObject(Dictionary<string, object> row)
        {
            Enfermero enfermero = new Enfermero();
            enfermero.Id = int.Parse(row["Id"].ToString());
            enfermero.IdUsuario = int.Parse(row["IdUsuario"].ToString());
            return enfermero;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_ENFERMERO";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }
        public SqlOperation GetDeleteStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_ENFERMERO";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }

        public SqlOperation GetRetrieveByUsuarioId(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_ENFERMERO_USUARIO_ID";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }
    }
}

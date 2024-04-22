using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class SecretarioMapper
    {
        public Secretario BuildObject(Dictionary<string, object> row)
        {
            Secretario secretario = new Secretario();
            secretario.Id = int.Parse(row["Id"].ToString());
            secretario.idUsuario = int.Parse(row["IdUsuario"].ToString());
            return secretario;
        }

        public List<Secretario> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_SECRETARIO";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }
        public SqlOperation GetDeleteStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_SECRETARIO";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }

        public SqlOperation GetRetrieveByUsuarioIdStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_SECRETARIO_USUARIO_ID";
            operation.AddIntegerParam("idUsuario",idUsuario);
            return operation;
        }
    }
}

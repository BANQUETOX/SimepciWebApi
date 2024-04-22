using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class AdministradorMapper 
    {
        public Administrador BuildObject(Dictionary<string, object> row)
        {
            Administrador administrador = new Administrador();
            administrador.Id = int.Parse(row["Id"].ToString());
            administrador.idUsuario = int.Parse(row["IdUsuario"].ToString());
            return administrador;
        }

        public List<Administrador> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Administrador> results = new List<Administrador>();

            foreach (var row in rowList)
            {
                var administrador = BuildObject(row);
                results.Add(administrador);
            }

            return results;
        }

        public SqlOperation GetCreateStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_ADMINISTRADOR";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_ADMINISTRADORES";
            return operation;
        }

        public SqlOperation GetDeleteStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_ADMINISTRADOR";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }

        public SqlOperation GetRetrieveByUsuarioId(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_ADMINISTRADOR_USUARIO_ID";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;
        }
    }
}

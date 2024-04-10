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
    public class EnfermeroMapper : IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            Paciente paciente = new Paciente();
            paciente.Id = int.Parse(row["Id"].ToString());
            paciente.IdUsuario = int.Parse(row["IdUsuario"].ToString());
            return paciente;
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
    }
}

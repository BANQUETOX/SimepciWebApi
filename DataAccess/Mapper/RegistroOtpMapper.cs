using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class RegistroOtpMapper : IObjectMapper, ICrudStatements
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            RegistroOtp registroOtp = new RegistroOtp();
            registroOtp.correoUsuario = row["Correo"].ToString() ;
            registroOtp.codigoOtp = row["Codigo"].ToString() ;
            return registroOtp;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(BaseClass dto)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_REGISTRO_OTP";
            RegistroOtp loginOtp = (RegistroOtp) dto;
            operation.AddVarcharParam("correoUsuario",loginOtp.correoUsuario);
            operation.AddVarcharParam("codigoOtp", loginOtp.codigoOtp);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveByEmailStatement(string correoUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_REGISTRO_OTP_CORREO";
            operation.AddVarcharParam("correoUsuario", correoUsuario);
            return operation;
        }
    }
}

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
    public class RecuperarPasswordOtpMapper : ICrudStatements, IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            RecuperarPasswordOtp recuperarPasswordOtp = new RecuperarPasswordOtp();
            recuperarPasswordOtp.codigo = row["Codigo"].ToString();
            return recuperarPasswordOtp;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<BaseClass> results = new List<BaseClass>();

            foreach (var row in rowList)
            {
                var passwordDto = BuildObject(row);
                results.Add(passwordDto);
            }

            return results;
        }

        public SqlOperation GetCreateStatement(BaseClass dto)
        {
            RecuperarPasswordOtp recuperarPasswordOtp = (RecuperarPasswordOtp)dto;
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_RECUPERAR_PASSWORD_OTP";
            operation.AddVarcharParam("codigo",recuperarPasswordOtp.codigo);
            
            return operation;
        }

        public SqlOperation GetByCodeStatement(string code)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_PASSWORD_OTP_CODE";
            operation.AddVarcharParam("codigo",code);
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
    }
}

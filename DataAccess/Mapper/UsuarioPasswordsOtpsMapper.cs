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
    public class UsuarioPasswordsOtpsMapper : ICrudStatements, IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            UsuarioPasswordsOtps usuarioPasswordsOtps = new UsuarioPasswordsOtps();
            usuarioPasswordsOtps.idUsuario = int.Parse(row["IdUsuario"].ToString());
            usuarioPasswordsOtps.idRecuperarPasswordOtp = int.Parse(row["IdRecuperarPasswordOtp"].ToString());
            return usuarioPasswordsOtps;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<BaseClass> results = new List<BaseClass>();

            foreach (var row in rowList)
            {
                var user = BuildObject(row);
                results.Add(user);
            }

            return results;
        }

        public SqlOperation GetCreateStatement(BaseClass dto)
        {
            UsuarioPasswordsOtps usuarioPasswordsOtps = (UsuarioPasswordsOtps)dto;
            SqlOperation sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_INSERT_USUARIO_PASSWORD_OTP";
            sqlOperation.AddIntegerParam("idUsuario", usuarioPasswordsOtps.idUsuario);
            sqlOperation.AddIntegerParam("idRecuperarPasswordOtp", usuarioPasswordsOtps.idRecuperarPasswordOtp);
            return sqlOperation;
        }

        public SqlOperation GetDeleteStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveByIdsStatement(int idUsuario,int idRecuperarPasswordOtp)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_USUARIO_PASSWORDS_OTPS";
            operation.AddIntegerParam("idUsuario",idUsuario);
            operation.AddIntegerParam("idRecuperarPasswordOtp",idRecuperarPasswordOtp);
            return operation;
            
        }

        public  SqlOperation GetRetrieveByIdStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }
    }
}

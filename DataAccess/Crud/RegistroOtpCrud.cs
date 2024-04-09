using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class RegistroOtpCrud 
    {
        RegistroOtpMapper mapper = new RegistroOtpMapper();
        SqlDao sqlDao = SqlDao.GetInstance();
        public void Create(BaseClass dto)
        {
            SqlOperation operation = mapper.GetCreateStatement(dto);
            sqlDao.ExecuteStoredProcedure(operation);
            
        }

        public RegistroOtp GetOtpByEmail(string correoUsuario)
        {
            SqlOperation operation = mapper.GetRetrieveByEmailStatement(correoUsuario);
            RegistroOtp registroOtp = (RegistroOtp)mapper.BuildObject(sqlDao.ExecuteStoredProcedureWithQuery(operation)[0]);
            return registroOtp;
           
        }
        public void Delete(string correo)
        {
            SqlOperation operation = mapper.GetDeleteOtpStatement(correo);
            sqlDao.ExecuteStoredProcedure(operation);
        }
    
    }
}

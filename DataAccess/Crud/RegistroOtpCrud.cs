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
    public class RegistroOtpCrud : CrudFactory
    {
        RegistroOtpMapper mapper = new RegistroOtpMapper();
        SqlDao sqlDao = SqlDao.GetInstance();
        public override void Create(BaseClass dto)
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

        public override void Delete(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseClass dto)
        {
            throw new NotImplementedException();
        }
    }
}

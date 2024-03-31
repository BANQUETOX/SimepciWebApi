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
    public class RecuperarPasswordOtpCrud : CrudFactory
    {
        internal RecuperarPasswordOtpMapper mapper;
        internal SqlDao sqlDao;

        public RecuperarPasswordOtpCrud()
        {
            mapper =new RecuperarPasswordOtpMapper();
            sqlDao = new SqlDao();
        }
        public override void Create(BaseClass dto)
        {
            SqlOperation operation = mapper.GetCreateStatement(dto);
            sqlDao.ExecuteStoredProcedure(operation);

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

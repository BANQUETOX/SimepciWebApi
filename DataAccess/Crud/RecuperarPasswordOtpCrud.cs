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
        public override void  Create(BaseClass dto)
        {
            SqlOperation operation = mapper.GetCreateStatement(dto);
            sqlDao.ExecuteStoredProcedure(operation);

        }

        public int CreateWithRetrieve(BaseClass dto)
        {
            SqlOperation operation = mapper.GetCreateStatement(dto);
            var passwordOtpId = sqlDao.ExecuteStoredProcedureWithQuery(operation);


            return int.Parse(passwordOtpId.First()["Id"].ToString());

        }

        public RecuperarPasswordOtp GetPasswordOtpByCode(string code)
        {
            SqlOperation operation = mapper.GetByCodeStatement(code);
            List<Dictionary<string, object>> dataResults = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            Console.WriteLine(dataResults);
            List<RecuperarPasswordOtp> resultList = new List<RecuperarPasswordOtp>();



            if (dataResults.Count > 0)
            {
                var dtoList = mapper.BuildObjects(dataResults);
                foreach (var passwordOtp in dtoList)
                {
                    resultList.Add((RecuperarPasswordOtp)Convert.ChangeType(passwordOtp, typeof(RecuperarPasswordOtp)));
                }
            }
            Console.WriteLine(resultList);
            Console.WriteLine(resultList[0].codigo);
            return resultList[0];

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

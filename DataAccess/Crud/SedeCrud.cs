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
    public class SedeCrud 
    {
        SedeMapper sedeMapper;
        SqlDao sqlDao;

        public SedeCrud() : base()
        {
            sedeMapper = new SedeMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public void Create(Sede sede)
        {
            SqlOperation operation = sedeMapper.GetCreateStatement(sede);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public void Delete(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveAll<T>()
        {
            List<T> resultList = new List<T>();
            SqlOperation operation = sedeMapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> dataResults = sqlDao.ExecuteStoredProcedureWithQuery(operation);

            if (dataResults.Count > 0)
            {
                var dtoList = sedeMapper.BuildObjects(dataResults);
                foreach (var dto in dtoList)
                {
                    resultList.Add((T)Convert.ChangeType(dto, typeof(T)));
                }
            }
            return resultList;
        }

        public  T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.Sedes;
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

        public SedeCrud() 
        {   
            sedeMapper = new SedeMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public void Create(Sede sede)
        {
            SqlOperation operation = sedeMapper.GetCreateStatement(sede);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public void Update(Sede sede) { 
            SqlOperation operation = sedeMapper.GetUpdateStatement(sede);
            sqlDao.ExecuteStoredProcedure(operation);
        }
        public void Delete(int idSede)
        {
            SqlOperation operation = sedeMapper.GetDeleteStatement(idSede);
            sqlDao.ExecuteStoredProcedure(operation);
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

        public  Sede RetrieveById(int idSede)
        {
            Sede sede = new Sede();
            SqlOperation operation = sedeMapper.GetRetrieveByIdStatement(idSede);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count > 0)
            {
                sede = sedeMapper.BuildObject(result[0]);
            }
            return sede;
        }
    }
}

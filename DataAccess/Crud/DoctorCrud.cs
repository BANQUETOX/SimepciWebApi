using DataAccess.Dao;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DataAccess.Crud
{
    public class DoctorCrud
    {
        DoctorMapper mapper;
        SqlDao sqlDao;

        public DoctorCrud()
        {
            mapper = new DoctorMapper();
            sqlDao = new SqlDao();
        }

        public void Create(Doctor doctor)
        {
            SqlOperation operation = mapper.GetCreateStatement(doctor);
            sqlDao.ExecuteStoredProcedure(operation);

        }

        public void Delete(int userId)
        {
            SqlOperation operation = mapper.GetDeleteStatement(userId);
            sqlDao.ExecuteStoredProcedure(operation);
        }
    }
}


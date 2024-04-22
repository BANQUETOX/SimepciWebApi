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
    public class EnfermeroCrud
    {
        EnfermeroMapper mapper;
        SqlDao sqlDao;

        public EnfermeroCrud()
        {
            mapper = new EnfermeroMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public void Create(int userId)
        {
            SqlOperation operation = mapper.GetCreateStatement(userId);
            sqlDao.ExecuteStoredProcedure(operation);

        }

        public void Delete(int userId)
        {
            SqlOperation operation = mapper.GetDeleteStatement(userId);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public Enfermero GetEnfermeroByUsuarioId(int idUsuario)
        {
            Enfermero enfermero = new Enfermero();
            SqlOperation operation = mapper.GetRetrieveByUsuarioId(idUsuario);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count() > 0)
            {
                enfermero = mapper.BuildObject(result[0]);
            }
            return enfermero;
        }
    }
}


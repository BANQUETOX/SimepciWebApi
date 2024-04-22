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
    public class SecretarioCrud
    {
        SecretarioMapper mapper;
        SqlDao sqlDao;

        public SecretarioCrud()
        {
            mapper = new SecretarioMapper();
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

        public Secretario GetSecretarioByUsuarioId(int idUsuario)
        {
            Secretario secretario = new Secretario();
            SqlOperation operation = mapper.GetRetrieveByUsuarioIdStatement(idUsuario);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count > 0)
            {
                secretario = mapper.BuildObject(result[0]);
            }
            return secretario;
        }

    }
}


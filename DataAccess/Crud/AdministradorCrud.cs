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
    public class AdministradorCrud
    {
        AdministradorMapper mapper;
        SqlDao sqlDao;

        public AdministradorCrud()
        {
            mapper = new AdministradorMapper();
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

        public List<Administrador> GetAllAdministrador()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            var administradores = mapper.BuildObjects(result);
            return administradores;
        }

        public Administrador GetAdministradorByUsuarioId(int idUsuario)
        {
            Administrador administrador = new Administrador();
            SqlOperation operation = mapper.GetRetrieveByUsuarioId(idUsuario);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count > 0)
            {
                administrador = mapper.BuildObject(result[0]);
            }
            return administrador;
        }
    }
}


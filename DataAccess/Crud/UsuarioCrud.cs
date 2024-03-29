using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;

namespace DataAccess.Crud
{
    public class UsuarioCrud : CrudFactory
    {

        UsuarioMapper usuarioMapper;

        public UsuarioCrud() : base()
        {
            usuarioMapper = new UsuarioMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseClass dto)
        {
            SqlOperation operation = usuarioMapper.GetCreateStatement(dto);
            dao.ExecuteStoredProcedure(operation);

        }

        public override void Delete(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {

            List<T> resultList = new List<T>();
            SqlOperation operation = usuarioMapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);

            if (dataResults.Count > 0)
            {
                var dtoList = usuarioMapper.BuildObjects(dataResults);
                foreach (var dto in dtoList)
                {
                    resultList.Add((T)Convert.ChangeType(dto, typeof(T)));
                }
            }
            return resultList;
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseClass dto)
        {
            throw new NotImplementedException();
        }


        public T Login<T>(string correo, string password)
        {
            List<T> resultList = new List<T>();
            SqlOperation operation = usuarioMapper.Login(correo,password);

            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);

            if (dataResults.Count > 0)
            {
                var dtoList = usuarioMapper.BuildObjects(dataResults);
                foreach (var dto in dtoList)
                {
                    resultList.Add((T)Convert.ChangeType(dto, typeof(T)));
                }
            }
            else
            {
                var dtoUsuario = usuarioMapper.BuildEmptyObject();
                resultList.Add((T)Convert.ChangeType(dtoUsuario, typeof(T)));
            }

            return resultList[0];
        }


        public void UpdatePassword(string correoUsuario, string newPassword)
        {
            SqlOperation operation = usuarioMapper.GetUpdatePasswordStatement(correoUsuario,newPassword);
            dao.ExecuteStoredProcedure(operation);
        }

    }
}

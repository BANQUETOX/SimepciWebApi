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
    public class RolCrud 
    {
        RolMapper rolMapper;
        SqlDao dao;
        public RolCrud() 
        {
            rolMapper = new RolMapper();
            dao = SqlDao.GetInstance();
        }

     

        public  List<T> RetrieveAll<T>()
        {
            List<T> resultList = new List<T>();
            SqlOperation operation = rolMapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);

            if (dataResults.Count > 0)
            {
                var dtoList = rolMapper.BuildObjects(dataResults);
                foreach (var dto in dtoList)
                {
                    resultList.Add((T)Convert.ChangeType(dto, typeof(T)));
                }
            }
            return resultList;
        }

      

        public string AsignarRolUsuario(int idUsuario, int idRol)
        {
            SqlOperation operation = rolMapper.GetAsignarRolStatement(idUsuario, idRol);
            dao.ExecuteStoredProcedure(operation);
            return "El rol a sido asignado";

        }

        public string RemoverRolUsuario(int idUsuario, int idRol)
        {
            SqlOperation operation = rolMapper.GetRemoverRolStatement(idUsuario, idRol);
            dao.ExecuteStoredProcedure(operation);
            return "El rol a sido removido";

        }

        public List<string> GetRolesUsuario(string correoUsuario)
        {
            SqlOperation operation = rolMapper.GetRolesUsuarioStatement(correoUsuario);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
            List<string> roles = new List<string>();
            foreach (var rol in dataResults)
            {
                roles.Add(rol["Nombre"].ToString());
            }
            return roles;

        }
    }
}

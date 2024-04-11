using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using DTO.Usuarios;

namespace DataAccess.Crud
{
    public class UsuarioCrud 
    {

        UsuarioMapper usuarioMapper;
        SqlDao dao;

        public UsuarioCrud() 
        {
            usuarioMapper = new UsuarioMapper();
            dao = SqlDao.GetInstance();
        }

        public  void Create(BaseClass dto)
        {
            SqlOperation operation = usuarioMapper.GetCreateStatement(dto);
            dao.ExecuteStoredProcedure(operation);

        }

        public  void Delete(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public  List<T> RetrieveAll<T>()
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

        public  Usuario RetrieveById(int id)
        {
            
            SqlOperation operation = usuarioMapper.GetRetrieveByIdStatement(id);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = (Usuario)usuarioMapper.BuildObject(result[0]);
            return usuario;
            
        }

        public  void Update(BaseClass dto)
        {
            throw new NotImplementedException();
        }


        public Usuario GetUsuarioByEmail(string correo)
        {
            SqlOperation operation = usuarioMapper.GetUserByEmailStatement(correo);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = (Usuario)usuarioMapper.BuildObject(dataResults[0]); 
            return usuario;
        }


        public Usuario Login(string correo, string password)
        {
            SqlOperation operation = usuarioMapper.Login(correo,password);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = new Usuario();

            if (dataResults.Count > 0)
            {
                 usuario = (Usuario)usuarioMapper.BuildObject(dataResults[0]);
                
            }
            else
            {
                usuario = (Usuario)usuarioMapper.BuildEmptyObject();
                
            }

            return usuario;
        }

        public string DesactivarUsuario(string correo)
        {
            SqlOperation operation = usuarioMapper.GetDesactivarUsuarioStatement(correo);
            dao.ExecuteStoredProcedureWithQuery(operation);
            return "El usuario a sido desactivado";
        }

        public string ActivarUsuario(string correo)
        {
            SqlOperation operation = usuarioMapper.GetActivarUsuarioStatement(correo);
            dao.ExecuteStoredProcedureWithQuery(operation);
            return "El usuario a sido activado";
        }


        public void UpdatePassword(string correoUsuario, string newPassword)
        {
            SqlOperation operation = usuarioMapper.GetUpdatePasswordStatement(correoUsuario,newPassword);
            dao.ExecuteStoredProcedure(operation);
        }

       

       

    }
}

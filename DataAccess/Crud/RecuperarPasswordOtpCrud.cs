using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
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

        public string GetPasswordOtpByEmail(string correo)
        {
            UsuarioCrud usuarioCrud = new UsuarioCrud();    
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(correo);
            SqlOperation operation = mapper.GetByUserIdStatement(usuario.Id);
            var dataResult = sqlDao.ExecuteStoredProcedureWithQuery(operation);
         
           
       
            return dataResult.First()["codigo"].ToString();

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

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
            sqlDao = SqlDao.GetInstance();
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

        public RecuperarPasswordOtp GetPasswordOtpByEmail(string correo)
        {
            UsuarioCrud usuarioCrud = new UsuarioCrud();    
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(correo);
            SqlOperation operation = mapper.GetByUserIdStatement(usuario.Id);
            RecuperarPasswordOtp otp = new RecuperarPasswordOtp();
            var dataResult = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (dataResult.Count()> 0) { 
                otp = (RecuperarPasswordOtp)mapper.BuildObject(dataResult[0]);
            
            }

            return otp;

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

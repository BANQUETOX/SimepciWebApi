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
    public class UsuarioPasswordOtpsCrud : CrudFactory
    {
        UsuarioPasswordsOtpsMapper mapper;
        SqlDao sqlDao;

        public UsuarioPasswordOtpsCrud()
        {
            mapper = new UsuarioPasswordsOtpsMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseClass dto)
        {
            SqlOperation operation = mapper.GetCreateStatement(dto);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public override void Delete(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public  List<UsuarioPasswordsOtps> RetrieveByIds(int idUsuario,int idRecuperarPasswordOtp)
        {   
            List<UsuarioPasswordsOtps> listResult = new List<UsuarioPasswordsOtps>();
            SqlOperation operation = mapper.GetRetrieveByIdsStatement(idUsuario,idRecuperarPasswordOtp);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);

            if (dataResults.Count > 0)
            {
                var dtoList = mapper.BuildObjects(dataResults);
                foreach (var dto in dtoList)
                {
                    listResult.Add((UsuarioPasswordsOtps)Convert.ChangeType(dto, typeof(UsuarioPasswordsOtps)));
                }
            }


            return listResult;

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

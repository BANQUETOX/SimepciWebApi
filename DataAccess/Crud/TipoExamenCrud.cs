using DataAccess.Dao;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class TipoExamenCrud
    {
        TipoExamenMapper mapper;
        SqlDao dao;
        public TipoExamenCrud()
        {
            mapper = new TipoExamenMapper();
            dao = SqlDao.GetInstance(); 
        }
    }
}

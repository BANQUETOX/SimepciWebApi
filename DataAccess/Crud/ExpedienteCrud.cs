using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.Expedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class ExpedienteCrud
    {
        ExpedienteMapper mapper = new ExpedienteMapper();
        SqlDao dao = new SqlDao();

        public void Create(Expediente expediente)
        {
            SqlOperation operation = mapper.GetCreateStatement(expediente);
            dao.ExecuteStoredProcedure(operation);
        }
    }
}

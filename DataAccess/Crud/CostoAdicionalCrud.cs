using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.CostosAdicionales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class CostoAdicionalCrud
    {
        CostoAdicionalMapper mapper;
        SqlDao sqlDao;

        public CostoAdicionalCrud()
        {
            mapper = new CostoAdicionalMapper();
            sqlDao = new SqlDao();
        }

        public void Create(CostoAdicional costoAdicional)
        {
            SqlOperation operation = mapper.GetCreateStatement(costoAdicional);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public List<CostoAdicional> GetCostosByFacutaId(int idFactura)
        {
            List<CostoAdicional> costosAdicionales = new List<CostoAdicional>();
            SqlOperation operation = mapper.GetRetrieveByFacturaId(idFactura);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            costosAdicionales = mapper.BuildObjects(result);
            return costosAdicionales;

        }
    }
}

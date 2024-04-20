using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.Facturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class FacturaCrud
    {

        FacturaMapper mapper;
        SqlDao sqlDao;

        public FacturaCrud()
        {
            mapper = new FacturaMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public Factura Create(Factura factura)
        {
            Factura facturaReturn = new Factura();
            SqlOperation operation = mapper.GetCreateStatement(factura);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count > 0) {
                facturaReturn = mapper.BuildObject(result[0]);
            }
            return facturaReturn;
        }
        public Factura GetFacturaById(int idFactura) {
            Factura factura = new Factura();
            SqlOperation operation = mapper.GetFacturaByIdPStatement(idFactura);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count() > 0) {
                factura = mapper.BuildObject(result[0]);    
            }
            return factura;
        
        }

        public List<Factura> GetFacturasPaciente(int idPaciente)
        {
            List<Factura> facturas = new List<Factura>();
            SqlOperation operation = mapper.GetFacturasPacienteStatement(idPaciente);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            facturas = mapper.BuildObjects(result);
            return facturas;
        }

      


    }
}

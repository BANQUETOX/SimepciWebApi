using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.Facturas;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public void UpdateFacturaPagada(int idFactura)
        {
            SqlOperation operation = mapper.GetUpdateSetPagadaStatement(idFactura);
            sqlDao.ExecuteStoredProcedure(operation);
        }
        public void UpdateFacturaSinPagar(int idFactura)
        {
            SqlOperation operation = mapper.GetUpdateSetSinPagarStatement(idFactura);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public List<Factura> GetFacturasPagadas()
        {
            SqlOperation operation = mapper.GetRetrieveFacturasPagadas();
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            return mapper.BuildObjects(result);
        }


        public Factura GetFacturaByCitaId(int idCita)
        {
            Factura factura = new Factura();
            SqlOperation operation = mapper.GetFacturaByCitaIdStatement(idCita);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count() > 0) { 
                factura = mapper.BuildObject(result[0]);
            }
            return factura;
        }


    }
}

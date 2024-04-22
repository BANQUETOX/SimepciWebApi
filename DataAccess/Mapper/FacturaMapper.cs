using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO.Facturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class FacturaMapper 
    {
        public Factura BuildObject(Dictionary<string, object> row)
        {
            Factura factura = new Factura();
            factura.Id = int.Parse(row["Id"].ToString());
            factura.monto = float.Parse(row["Monto"].ToString());
            factura.fechaEmision = DateTime.Parse(row["FechaEmision"].ToString());
            factura.idCita = int.Parse(row["IdCita"].ToString());
            factura.pagada = bool.Parse(row["Pagada"].ToString());
            return factura;
        }

        public List<Factura> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Factura> results = new List<Factura>();

            foreach (var row in rowList)
            {
                var factura = BuildObject(row);
                results.Add(factura);
            }

            return results;
        }


        public SqlOperation GetCreateStatement(Factura factura)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_FACTURA";
            operation.AddFloatParam("monto",factura.monto);
            operation.AddDatetimeParam("fechaEmision",factura.fechaEmision);
            operation.AddIntegerParam("idCita",factura.idCita);
            operation.AddBooleanParam("pagada", factura.pagada);
            return operation;
        }

        public SqlOperation GetFacturaByIdPStatement(int idFactura)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_FACTURA_ID";
            operation.AddIntegerParam("id", idFactura);
            return operation;

        }
        public SqlOperation GetFacturasPacienteStatement(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_FACTURAS_PACIENTE_ID";
            operation.AddIntegerParam("idPaciente", idPaciente);
            return operation;

        }

        public SqlOperation GetFacturaByCitaIdStatement(int idCita)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_FACTURA_CITA_ID";
            operation.AddIntegerParam("idCita", idCita);
            return operation;

        }

        public SqlOperation GetUpdateSetPagadaStatement(int idFactura)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_FACTURA_PAGADA";
            operation.AddIntegerParam("id",idFactura);
            return operation;

        }

        public SqlOperation GetUpdateSetSinPagarStatement(int idFactura)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_FACTURA_SIN_PAGAR";
            operation.AddIntegerParam("id", idFactura);
            return operation;

        }

        public SqlOperation GetRetrieveFacturasPagadas()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_FACTURAS_PAGADAS";
            return operation;
        }
    }
}

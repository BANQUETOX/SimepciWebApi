using DataAccess.Dao;
using DTO.CostosAdicionales;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class CostoAdicionalMapper
    {
        public CostoAdicional BuildObject(Dictionary<string, object> row)
        {
            CostoAdicional costoAdicional = new CostoAdicional();
            costoAdicional.Id = int.Parse(row["Id"].ToString());
            costoAdicional.idFactura = int.Parse(row["IdFactura"].ToString());
            costoAdicional.nombre = row["Nombre"].ToString();
            costoAdicional.precio = float.Parse(row["Precio"].ToString());
            return costoAdicional;
        }

        public List<CostoAdicional> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<CostoAdicional> results = new List<CostoAdicional>();

            foreach (var row in rowList)
            {
                var costoAdicional = BuildObject(row);
                results.Add(costoAdicional);
            }

            return results;
        }


        public SqlOperation GetCreateStatement(CostoAdicional costoAdicional)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_COSTO_ADICIONAL";
            operation.AddIntegerParam("idFactura",costoAdicional.idFactura);
            operation.AddVarcharParam("nombre", costoAdicional.nombre);
            operation.AddFloatParam("precio", costoAdicional.precio);
            return operation;
        }


        public SqlOperation GetRetrieveByFacturaId(int idFactura)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_COSTOS_ADICIONALES_FACTURA_ID";
            operation.AddIntegerParam("idFactura",idFactura);
            return operation;
        }
    }


}

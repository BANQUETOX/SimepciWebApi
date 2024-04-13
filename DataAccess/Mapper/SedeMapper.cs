using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO.Sedes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class SedeMapper 
    {
        public Sede BuildObject(Dictionary<string, object> row)
        {
            Sede sede = new Sede();
            sede.Id = int.Parse(row["Id"].ToString());
            sede.nombre = row["Nombre"].ToString();
            sede.descripcion = row["Descripcion"].ToString();
            sede.fechaCreacion = DateTime.Parse(row["FechaCreacion"].ToString());
            sede.ubicacion = row["Ubicacion"].ToString();
            sede.foto = row["Foto"].ToString();
            return sede;
        }

        public List<Sede> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Sede> listaSedes = new List<Sede>();
            foreach (var row in rowList) { 
                var sede = BuildObject(row);
                listaSedes.Add(sede);
            }
            return listaSedes;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_SEDES";
            return operation;
        }

        public SqlOperation GetCreateStatement(Sede sede)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_SEDE";
            operation.AddVarcharParam("nombre", sede.nombre);
            operation.AddVarcharParam("descripcion", sede.descripcion);
            operation.AddVarcharParam("ubicacion",sede.ubicacion);
            operation.AddVarcharParam("foto", sede.foto);
            operation.AddDatetimeParam("fechaCreacion", sede.fechaCreacion);
            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int idSede)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_SEDE_ID";
            operation.AddIntegerParam("idSede",idSede);
            return operation;
        }

        public SqlOperation GetUpdateStatement(Sede sede)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_SEDE";
            operation.AddIntegerParam("idSede",sede.Id);
            operation.AddVarcharParam("nombre", sede.nombre);
            operation.AddVarcharParam("descripcion", sede.descripcion);
            operation.AddVarcharParam("ubicacion", sede.ubicacion);
            operation.AddVarcharParam("foto", sede.foto);
            operation.AddDatetimeParam("fechaCreacion", sede.fechaCreacion);
            return operation;
        }

        public SqlOperation GetDeleteStatement(int idSede)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_SEDE";
            operation.AddIntegerParam("idSede", idSede);
            return operation;
        }



    }
}

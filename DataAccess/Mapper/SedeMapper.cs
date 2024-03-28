using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class SedeMapper : ICrudStatements, IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            Sede sede = new Sede();
            sede.nombre = row["Nombre"].ToString();
            sede.descripcion = row["Descripcion"].ToString();
            sede.fechaCreacion = DateTime.Parse(row["FechaCreacion"].ToString());
            sede.ubicacion = row["Ubicacion"].ToString();
            sede.foto = row["Foto"].ToString();
            sede.referencias = row["Referencias"].ToString();
            return sede;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<BaseClass> listaSedes = new List<BaseClass>();
            foreach (var row in rowList) { 
                var sede = BuildObject(row);
                listaSedes.Add(sede);
            }
            return listaSedes;
        }

        public SqlOperation GetCreateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_SEDES";
            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }
    }
}

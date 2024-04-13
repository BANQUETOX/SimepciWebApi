using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO.Recetas;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class RecetaMapper 
    {
        public Receta BuildObject(Dictionary<string, object> row)
        {
            Receta receta = new Receta();
            receta.Id = int.Parse(row["Id"].ToString());
            receta.idPaciente = int.Parse(row["IdPaciente"].ToString());
            receta.imagen = row["Imagen"].ToString();
            receta.fechaEmision = DateTime.Parse(row["FechaEmision"].ToString());
            receta.medicamento = row["Medicamento"].ToString();
            receta.dosis = row["Dosis"].ToString();
            receta.diasDosis = row["DiasDosis"].ToString();
            receta.recomendaciones = row["Recomendaciones"].ToString();
            return receta;
        }

        public List<Receta> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Receta> results = new List<Receta>();

            foreach (var row in rowList)
            {
                var receta = BuildObject(row);
                results.Add(receta);
            }

            return results;
        }

        public SqlOperation Create(Receta receta)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_RECETA";
            operation.AddIntegerParam("idPaciente",receta.idPaciente);
            operation.AddVarcharParam("imagen", receta.imagen);
            operation.AddDatetimeParam("fechaEmision",receta.fechaEmision);
            operation.AddVarcharParam("medicamento",receta.medicamento);
            operation.AddVarcharParam("dosis", receta.dosis);
            operation.AddVarcharParam("diasDosis",receta.diasDosis);
            operation.AddVarcharParam("recomendaciones",receta.recomendaciones);
            return operation;
        }

        public SqlOperation GetRecetasByPaciente(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_RECETA_PACIENTE_ID";
            operation.AddIntegerParam("idPaciente",idPaciente);
            return operation;
        }

        public SqlOperation GetUpdateRecetaStatement(Receta receta)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_RECETA";
            operation.AddIntegerParam("idReceta", receta.Id);
            operation.AddIntegerParam("idPaciente", receta.idPaciente);
            operation.AddVarcharParam("imagen", receta.imagen);
            operation.AddDatetimeParam("fechaEmision", receta.fechaEmision);
            operation.AddVarcharParam("medicamento", receta.medicamento);
            operation.AddVarcharParam("dosis", receta.dosis);
            operation.AddVarcharParam("diasDosis", receta.diasDosis);
            operation.AddVarcharParam("recomendaciones", receta.recomendaciones);
            return operation;
        }


        public SqlOperation GetDeleteStatement(int idReceta)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_RECETA";
            operation.AddIntegerParam("idReceta",idReceta);
            return operation;
        }


        public SqlOperation GetRertieveByIdStatement(int idReceta)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_RECETA_ID";
            operation.AddIntegerParam("idReceta", idReceta);
            return operation;
        }
    }
}

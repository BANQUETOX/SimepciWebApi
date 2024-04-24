using DataAccess.Dao;
using DTO.Diagnosticos;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class DiagnosticoMapper
    {
        public Diagnostico BuildObject(Dictionary<string, object> row)
        {
            Diagnostico diagnostico = new Diagnostico();
            diagnostico.Id = int.Parse(row["Id"].ToString());
            diagnostico.nombre = row["Nombre"].ToString();
            diagnostico.descripcion = row["Descripcion"].ToString();
            diagnostico.fechaEmision = DateTime.Parse(row["FechaEmision"].ToString());
            diagnostico.idPaciente = int.Parse(row["IdPaciente"].ToString());
            return diagnostico;
        }
        public List<Diagnostico> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Diagnostico> results = new List<Diagnostico>();
            foreach (var row in rowList)
            {
                var diagnostico = BuildObject(row);
                results.Add(diagnostico);
            }

            return results;
        }

        public SqlOperation GetCreateStatement(Diagnostico diagnostico)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_DIAGNOSTICO";
            operation.AddIntegerParam("idPaciente",diagnostico.idPaciente);
            operation.AddVarcharParam("nombre",diagnostico.nombre);
            operation.AddVarcharParam("descripcion",diagnostico.descripcion);
            operation.AddDatetimeParam("fechaEmision",diagnostico.fechaEmision);
            return operation;

        }

        public SqlOperation GetRetrieveDiagnosticosByPacienteId(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_DIAGNOSTICO_PACIENTE_ID";
            operation.AddIntegerParam("idPaciente",idPaciente);
            return operation;

        }
    }
}

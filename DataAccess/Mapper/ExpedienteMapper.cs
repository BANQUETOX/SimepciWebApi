using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO.Expedientes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class ExpedienteMapper
    {
        public Expediente BuildObject(Dictionary<string, object> row)
        {
            Expediente expediente = new Expediente();
            expediente.idPaciente = int.Parse(row["IdPaciente"].ToString());
            expediente.notasEnfermeria = row["NotasEnfermeria"].ToString();
            expediente.notasMedicas = row["NotasMedicas"].ToString();
            expediente.historialMedico = row["HistorialMedico"].ToString();
            return expediente;

        }

        public List<Expediente> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Expediente> results = new List<Expediente>();

            foreach (var row in rowList)
            {
                var expediente = BuildObject(row);
                results.Add(expediente);
            }

            return results;
        }

        public SqlOperation GetCreateStatement(Expediente expediente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_EXPEDIENTE";
            operation.AddIntegerParam("idPaciente",expediente.idPaciente);
            operation.AddVarcharParam("notasEnfermeria",expediente.notasEnfermeria);
            operation.AddVarcharParam("notasMedicas", expediente.notasMedicas);
            operation.AddVarcharParam("historialMedico", expediente.historialMedico);
            return operation;
        }

        public SqlOperation GetInitialUpdateStatement(Expediente expediente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_EXPEDIENTE";
            operation.AddIntegerParam("idPaciente", expediente.idPaciente);
            operation.AddVarcharParam("notasEnfermeria", expediente.notasEnfermeria);
            operation.AddVarcharParam("notasMedicas", expediente.notasMedicas);
            operation.AddVarcharParam("historialMedico", expediente.historialMedico);
            return operation;
        }

        public SqlOperation GetExpedienteByPacienteStatement(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_EXPEDIENTE_PACIENTE";
            operation.AddIntegerParam("idPaciente",idPaciente);
            return operation;

        }

    }
}

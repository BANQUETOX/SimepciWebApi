using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using DTO.ExamenesMedicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class ExamenMedicoMapper 
    {
        public ExamenMedico BuildObject(Dictionary<string, object> row)
        {
            ExamenMedico examenMedico = new ExamenMedico();
            examenMedico.Id = int.Parse(row["Id"].ToString());
            examenMedico.idPaciente = int.Parse(row["IdPaciente"].ToString());
            examenMedico.idTipoExamenMedico = int.Parse(row["IdTipoExamen"].ToString());
            examenMedico.resultado = row["Resultado"].ToString();
            examenMedico.objetivo = row["Objetivo"].ToString();
            return examenMedico;
        }

        public List<ExamenMedico> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<ExamenMedico> results = new List<ExamenMedico>();

            foreach (var row in rowList)
            {
                var examenMedico = BuildObject(row);
                results.Add(examenMedico);
            }

            return results;
        }

        public SqlOperation GetRetrieveByPacienteIdOperation(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_EXAMEN_MEDICO_PACIENTE_ID";
            operation.AddIntegerParam("idPaciente", idPaciente);
            return operation;
        }
        public SqlOperation GetCreateStatement(ExamenMedico examenMedico)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_EXAMEN_MEDICO";
            operation.AddIntegerParam("idPaciente", examenMedico.idPaciente);
            operation.AddIntegerParam("idTipoExamen", examenMedico.idTipoExamenMedico);
            operation.AddVarcharParam("resultado", examenMedico.resultado);
            operation.AddVarcharParam("objetivo", examenMedico.objetivo);
            return operation;

        }
         
        public SqlOperation GetUpdateStatement(ExamenMedico examenMedico)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_EXAMEN_MEDICO";
            operation.AddIntegerParam("id", examenMedico.Id);
            operation.AddVarcharParam("resultado", examenMedico.resultado);
            return operation;

        }

        public SqlOperation GetRetrieveByIdStatement (int idExamenMedico)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_EXAMEN_MEDICO_ID";
            operation.AddIntegerParam ("id", idExamenMedico);
            return operation;
        }

        public SqlOperation GetDeleteStatement(int idExamenMedico)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName= "SP_DELETE_EXAMEN_MEDICO";
            operation.AddIntegerParam("id", idExamenMedico);
            return operation;
        }
       
    }

}

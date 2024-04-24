using DataAccess.Dao;
using DTO.InfosExpediente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class InfoExpedienteMapper
    {
        public InfoExpediente BuildObject(Dictionary<string, object> row)
        {
            InfoExpediente infoExpediente = new InfoExpediente();
            infoExpediente.Id = int.Parse(row["Id"].ToString());
            infoExpediente.idPaciente = int.Parse(row["IdPaciente"].ToString());
            infoExpediente.contenido = row["Contenido"].ToString();
            infoExpediente.fechaEmision = DateTime.Parse(row["FechaEmision"].ToString());
            return infoExpediente;
        }

        public List<InfoExpediente> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<InfoExpediente> results = new List<InfoExpediente>();

            foreach (var row in rowList)
            {
                var infoExpediente = BuildObject(row);
                results.Add(infoExpediente);
            }

            return results;
        }

        public SqlOperation GetCreateNotaMedicaStatement(InfoExpediente notaMedica)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_NOTA_MEDICA";
            operation.AddIntegerParam("idPaciente",notaMedica.idPaciente);
            operation.AddVarcharParam("contenido",notaMedica.contenido);
            operation.AddDatetimeParam("fechaEmision", notaMedica.fechaEmision);
            return operation;
        }

        public SqlOperation GetCreateNotaEnfermeriaStatement(InfoExpediente notaEnfermeria)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_NOTA_ENFERMERIA";
            operation.AddIntegerParam("idPaciente", notaEnfermeria.idPaciente);
            operation.AddVarcharParam("contenido", notaEnfermeria.contenido);
            operation.AddDatetimeParam("fechaEmision", notaEnfermeria.fechaEmision);
            return operation;
        }

        public SqlOperation GetCreateHistorialMedico(InfoExpediente historialMedico)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_HISTORIAL_MEDICO";
            operation.AddIntegerParam("idPaciente", historialMedico.idPaciente);
            operation.AddVarcharParam("contenido", historialMedico.contenido);
            operation.AddDatetimeParam("fechaEmision", historialMedico.fechaEmision);
            return operation;
        }
       /*-----------------------------Retrieves----------------------------------------------------*/

        public SqlOperation GetRetrieveNotaEnfermeria(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_NOTA_ENFERMERIA_PACIENTE_ID";
            operation.AddIntegerParam("idPaciente",idPaciente);
            return operation;
        }

        public SqlOperation GetRetrieveNotaMedica(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_NOTA_MEDICA_PACIENTE_ID";
            operation.AddIntegerParam("idPaciente", idPaciente);
            return operation;
        }

        public SqlOperation GetRetrieveHistorialMedico(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_HISTORIAL_MEDICO_PACIENTE_ID";
            operation.AddIntegerParam("idPaciente", idPaciente);
            return operation;
        }




    }
}

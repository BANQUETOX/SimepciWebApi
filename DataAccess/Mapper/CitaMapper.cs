using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using DTO.Citas;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class CitaMapper 
    {
        public Cita BuildObject(Dictionary<string, object> row)
        {
            Cita cita = new Cita();
            cita.Id = int.Parse(row["Id"].ToString());
            cita.idPaciente = int.Parse(row["IdPaciente"].ToString());
            cita.idDoctor = int.Parse(row["IdDoctor"].ToString());
            cita.horaInicio = DateTime.Parse(row["HoraInicio"].ToString());
            cita.horaFinal = DateTime.Parse(row["HoraFinal"].ToString());
            cita.idSede = int.Parse(row["IdSede"].ToString());
            return cita;
        }

        public List<Cita> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Cita> results = new List<Cita>();

            foreach (var row in rowList)
            {
                var rol = BuildObject(row);
                results.Add(rol);
            }

            return results;
        }


        public SqlOperation RetrieveByIdStatement(int id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_CITA_ID";
            operation.AddIntegerParam("id", id);
            return operation;
        }
        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_CITAS";
            return operation;
        }

        public SqlOperation GetCreateStatement(Cita cita)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_CITA";
            operation.AddIntegerParam("idPaciente", cita.idPaciente);
            operation.AddIntegerParam("idDoctor", cita.idDoctor);
            operation.AddIntegerParam("idSede", cita.idSede);
            operation.AddDatetimeParam("horaInicio", cita.horaInicio);
            operation.AddDatetimeParam("horaFinal", cita.horaFinal);
            return operation;

        }

        public SqlOperation GetUpdateCitaStatement(Cita cita)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_CITA";
            operation.AddIntegerParam("idPaciente", cita.idPaciente);
            operation.AddIntegerParam("idDoctor", cita.idDoctor);
            operation.AddIntegerParam("idSede", cita.idSede);
            operation.AddDatetimeParam("horaInicio", cita.horaInicio);
            operation.AddDatetimeParam("horaFinal", cita.horaFinal);
            return operation;
        }
        public SqlOperation GetDeleteStatement(int id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_CITA";
            operation.AddIntegerParam("id", id);
            return operation;
        }

        public SqlOperation GetCitasReservadasStatement(DateTime fechaInico, DateTime fechaFinal,int idEspecialidad, int idSede) {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_CITAS_FECHA";
            operation.AddDatetimeParam("fechaInicio", fechaInico);
            operation.AddDatetimeParam("fechaFinal",fechaFinal);
            operation.AddIntegerParam("idEspecialidad", idEspecialidad);
            operation.AddIntegerParam("idSede", idSede);
            return operation;

        
        }

        public SqlOperation GetCitasPacienteStatement(int idPaciente)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_CITAS_PACIENTE";
            operation.AddIntegerParam("idPaciente",idPaciente);
            return operation;
        }
        public SqlOperation GetCitasDoctorStatement(int idDoctor)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_CITAS_DOCTOR";
            operation.AddIntegerParam("idDoctor", idDoctor);
            return operation;
        }


      
    }
}

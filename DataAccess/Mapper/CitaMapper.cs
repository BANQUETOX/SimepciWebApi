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
    public class CitaMapper : IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            Cita cita = new Cita();
            cita.idPaciente = int.Parse(row["IdPaciente"].ToString());
            cita.idDoctor = int.Parse(row["IdDoctor"].ToString());
            cita.horaInicio = DateTime.Parse(row["HoraInicio"].ToString());
            cita.horaFinal = DateTime.Parse(row["HoraFinal"].ToString());
            cita.idSede = int.Parse(row["IdSede"].ToString());
            return cita;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<BaseClass> results = new List<BaseClass>();

            foreach (var row in rowList)
            {
                var rol = BuildObject(row);
                results.Add(rol);
            }

            return results;
        }

        public SqlOperation Create(Cita cita)
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

        SqlOperation UpdateCita(Cita cita)
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
        public SqlOperation Delete(int id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_CITA";
            operation.AddIntegerParam("id", id);
            return operation;
        }
    }
}

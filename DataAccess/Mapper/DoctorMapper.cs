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
    public class DoctorMapper : IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            Doctor doctor = new Doctor();
            doctor.Id = int.Parse(row["Id"].ToString());
            doctor.idUsuario = int.Parse(row["IdUsuario"].ToString());
            doctor.idEspecialidad = int.Parse(row["IdEspecialidad"].ToString());
            doctor.idSede = int.Parse(row["IdSede"].ToString());
            doctor.horario = int.Parse(row["Horario"].ToString());
            return doctor;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(Doctor doctor)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_DOCTOR";
            operation.AddIntegerParam("idUsuario", doctor.idUsuario);
            operation.AddIntegerParam("idEspecialidad",doctor.idEspecialidad);
            operation.AddIntegerParam("idSede",doctor.idSede);
            operation.AddIntegerParam("horario",doctor.horario);
            return operation;

        }
        public SqlOperation GetDeleteStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_DOCTOR";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }
    }
}

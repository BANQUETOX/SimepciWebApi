using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO.Doctores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class DoctorMapper 
    {
        public Doctor BuildObject(Dictionary<string, object> row)
        {
            Doctor doctor = new Doctor();
            doctor.Id = int.Parse(row["Id"].ToString());
            doctor.idUsuario = int.Parse(row["IdUsuario"].ToString());
            doctor.idEspecialidad = int.Parse(row["IdEspecialidad"].ToString());
            doctor.idSede = int.Parse(row["IdSede"].ToString());
            doctor.horario = int.Parse(row["Horario"].ToString());
            return doctor;
        }

        public List<Doctor> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Doctor> results = new List<Doctor>();

            foreach (var row in rowList)
            {
                var doctor = BuildObject(row);
                results.Add(doctor);
            }

            return results;
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

        public SqlOperation GetDoctorByIdStatement(int idDoctor)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_DOCTOR_ID";
            operation.AddIntegerParam("idDoctor",idDoctor);
            return operation;
        }

        public SqlOperation GetDoctorBySedeAndEspecialidadStatement(int idSede,int idEspecialidad)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_DOCTORES_SEDE_ESPECIALIDAD";
            operation.AddIntegerParam("idSede",idSede);
            operation.AddIntegerParam("idEspecialidad",idEspecialidad);
            return operation;
        }

        public SqlOperation GetDoctorByUsuarioIdStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_DOCTOR_USUARIO_ID";
            operation.AddIntegerParam("idUsuario",idUsuario);
            return operation;
        }

        public SqlOperation GetUpdateHorarioDoctor(int idDoctor, int horarioNuevo)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_DOCTOR_HORARIO";
            operation.AddIntegerParam("idDoctor", idDoctor);
            operation.AddIntegerParam("horario",horarioNuevo);
            return operation;
        }


        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_DOCTORES";
            return operation;
        }
    }
}

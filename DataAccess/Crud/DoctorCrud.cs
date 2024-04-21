using DataAccess.Dao;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Diagnostics.CodeAnalysis;
namespace DataAccess.Crud
{
    public class DoctorCrud
    {
        DoctorMapper mapper;
        SqlDao sqlDao;

        public DoctorCrud()
        {
            mapper = new DoctorMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public void Create(Doctor doctor)
        {
            SqlOperation operation = mapper.GetCreateStatement(doctor);
            sqlDao.ExecuteStoredProcedure(operation);

        }

        public Doctor GetDoctorById(int idDoctor)
        {
            SqlOperation operation = mapper.GetDoctorByIdStatement(idDoctor);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            Doctor doctor = new Doctor();
            if (result.Count > 0)
            {
                doctor = mapper.BuildObject(result[0]);
            }
            return doctor;
        }

        public void Delete(int userId)
        {
            SqlOperation operation = mapper.GetDeleteStatement(userId);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public List<Doctor> DoctoresBySedeAndEspecialidad(int idSede, int idEspecialidad) {
            SqlOperation operation = mapper.GetDoctorBySedeAndEspecialidadStatement(idSede, idEspecialidad);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            return mapper.BuildObjects(result);
        }

        public Doctor GetDoctorByUsuarioId(int idUsuario)
        {
            SqlOperation operation = mapper.GetDoctorByUsuarioIdStatement(idUsuario);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            Doctor doctor = new Doctor();
            if (result.Count > 0)
            {
                doctor = mapper.BuildObject(result[0]);
            }
            return doctor;

        }
    }
}


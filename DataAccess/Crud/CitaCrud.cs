using DataAccess.Dao;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Citas;
using DTO;

namespace DataAccess.Crud
{
    public class CitaCrud
    {
        CitaMapper citaMapper;
        SqlDao sqlDao;

        public CitaCrud()
        {
            citaMapper = new CitaMapper();
            sqlDao =  SqlDao.GetInstance();
        }

        public void Create(Cita cita)
        {
            SqlOperation operation = citaMapper.GetCreateStatement(cita);
            sqlDao.ExecuteStoredProcedure(operation);
            
        }

        public List<Cita> GetAllCitas() {
            SqlOperation operation = citaMapper.GetRetrieveAllStatement();
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            return citaMapper.BuildObjects(result);


        }

        public List<Cita> GetCitasReservadas(DateTime fechaInico, DateTime fechaFinal,int idEspecialidad, int idSede) {
            SqlOperation operation = citaMapper.GetCitasReservadasStatement(fechaInico,fechaFinal, idEspecialidad, idSede);
            var results = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            return citaMapper.BuildObjects(results); 

        }

        public List<Cita> GetCitasPaciente(int idPaciente)
        {
            SqlOperation operation = citaMapper.GetCitasPacienteStatement(idPaciente);
            var results = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            return citaMapper.BuildObjects(results);
        }
        public List<Cita> GetCitasDoctor(int idDoctor)
        {
            SqlOperation operation = citaMapper.GetCitasDoctorStatement(idDoctor);
            var results = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            return citaMapper.BuildObjects(results);
        }

        public Cita GetCitaById(int id)
        {
            Cita cita = null;
            SqlOperation operation = citaMapper.GetCitasDoctorStatement(id);
            var results = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if(results.Count() > 0)
            {
                cita = citaMapper.BuildObject(results[0]);
            }
            return cita;
        }

        public void DeleteCita(int id)
        {
            SqlOperation operation = citaMapper.GetDeleteStatement(id);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public Cita GetCitaByDoctorFecha(int idDoctor, DateTime horaInico, DateTime horaFinal)
        {   
            Cita cita = new Cita();
            SqlOperation operation = citaMapper.GetCitaDoctroHoraStatement(idDoctor,horaInico,horaFinal);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count() > 0) {
                cita = citaMapper.BuildObject(result[0]);
            }
            return cita;
          
        }

    }
}

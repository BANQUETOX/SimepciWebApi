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
            SqlOperation operation = citaMapper.GetCitasPacienteStatement(idDoctor);
            var results = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            return citaMapper.BuildObjects(results);
        }
    }
}

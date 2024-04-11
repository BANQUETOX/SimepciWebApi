using DataAccess.Dao;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Citas;

namespace DataAccess.Crud
{
    public class CitaCrud
    {
        CitaMapper citaMapper;
        SqlDao sqlDao;

        public CitaCrud()
        {
            citaMapper = new CitaMapper();
            sqlDao = new SqlDao();
        }

        public void Create(Cita cita)
        {
            SqlOperation operation = citaMapper.Create(cita);
            sqlDao.ExecuteStoredProcedure(operation);
            
        }

        public List<Cita> GetCitasReservadas(DateTime fechaInico, DateTime fechaFinal,int idEspecialidad, int idSede) {
            List<Cita> citas = new List<Cita>();
            SqlOperation operation = citaMapper.GetCitasReservadas(fechaInico,fechaFinal, idEspecialidad, idSede);
            var results = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            return citaMapper.BuildObjects(results); ;

        }
    }
}

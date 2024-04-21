using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class PacienteCrud
    {
        PacienteMapper mapper;
        SqlDao sqlDao;

        public PacienteCrud()
        {
            mapper = new PacienteMapper();
            sqlDao = SqlDao.GetInstance();
        }

      

        public void Create(int userId)
        {
            SqlOperation operation = mapper.GetCreateStatement(userId);
            sqlDao.ExecuteStoredProcedure(operation);

        }

        public void Delete(int userId)
        {
            SqlOperation operation = mapper.GetDeleteStatement(userId);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public  List<T> RetrieveAll<T>()
        {

            List<T> resultList = new List<T>();
            SqlOperation operation = mapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> dataResults = sqlDao.ExecuteStoredProcedureWithQuery(operation);

            if (dataResults.Count > 0)
            {
                var dtoList = mapper.BuildObjects(dataResults);
                foreach (var dto in dtoList)
                {
                    resultList.Add((T)Convert.ChangeType(dto, typeof(T)));
                }
            }
            return resultList;
        }

        public Paciente GetPacieteByUsuarioId(int usuarioId)
        {
            Paciente paciente = new Paciente();
            SqlOperation operation = mapper.GetByUsuarioIdStatement(usuarioId);
            List<Dictionary<string, object>> dataResults = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (dataResults.Count > 0)
            {
                paciente = (Paciente)mapper.BuildObject(dataResults[0]);

            }

            return paciente;


        }


        public Paciente GetPacienteById(int id)
        {
            Paciente paciente = new Paciente();
            SqlOperation operation = mapper.GetRetrieveByIdStatement(id);
            List<Dictionary<string, object>> dataResults = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (dataResults.Count > 0)
            {
                paciente = (Paciente)mapper.BuildObject(dataResults[0]);

            }

            return paciente;

        }
    }
}

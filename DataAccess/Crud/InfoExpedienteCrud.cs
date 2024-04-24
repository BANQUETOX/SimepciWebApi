using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.InfosExpediente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class InfoExpedienteCrud
    {
        InfoExpedienteMapper mapper;
        SqlDao sqlDao;

        public InfoExpedienteCrud()
        {
            mapper = new InfoExpedienteMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public void CreateNotaMedica(InfoExpediente notaMedica)
        {
            SqlOperation operation = mapper.GetCreateNotaMedicaStatement(notaMedica);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public void CreateNotaEnfermeria(InfoExpediente notaEnfermeria)
        {
            SqlOperation operation = mapper.GetCreateNotaEnfermeriaStatement(notaEnfermeria);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public void CreateHistorialMedico(InfoExpediente historialMedico)
        {
            SqlOperation operation = mapper.GetCreateHistorialMedico(historialMedico);
            sqlDao.ExecuteStoredProcedure(operation);   
        }


        /*---------------------------GETS--------------------------------------*/

        public List<InfoExpediente> GetNotasMedicas(int idPaciente)
        {
            List<InfoExpediente> infoExpediente = new List<InfoExpediente>();
            SqlOperation operation = mapper.GetRetrieveNotaMedica(idPaciente);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            infoExpediente = mapper.BuildObjects(result);
            return infoExpediente;
        }

        public List<InfoExpediente> GetNotasEnfermeria(int idPaciente)
        {
            List<InfoExpediente> infoExpediente = new List<InfoExpediente>();
            SqlOperation operation = mapper.GetRetrieveNotaEnfermeria(idPaciente);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            infoExpediente = mapper.BuildObjects(result);
            return infoExpediente;
        }

        public List<InfoExpediente> GetHistorialMedico(int idPaciente)
        {
            List<InfoExpediente> infoExpediente = new List<InfoExpediente>();
            SqlOperation operation = mapper.GetRetrieveHistorialMedico(idPaciente);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            infoExpediente = mapper.BuildObjects(result);
            return infoExpediente;
        }


    }
}

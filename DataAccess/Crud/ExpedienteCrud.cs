using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.Expedientes;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class ExpedienteCrud
    {
        ExpedienteMapper mapper;
        SqlDao dao;

        public ExpedienteCrud()
        {
            mapper = new ExpedienteMapper();
            dao = SqlDao.GetInstance();
        }

        public void Create(Expediente expediente)
        {
            SqlOperation operation = mapper.GetCreateStatement(expediente);
            dao.ExecuteStoredProcedure(operation);
        }

        public Expediente GetExpedietePaciente(int idPaciente) {
            Expediente expediente = new Expediente();
            SqlOperation operation = mapper.GetExpedienteByPacienteStatement(idPaciente);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
            if (dataResults.Count > 0)
            {
                expediente = (Expediente)mapper.BuildObject(dataResults[0]);

            }
            return expediente;

        }
    }
}

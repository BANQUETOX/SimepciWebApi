using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.ExamenesMedicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class ExamenMedicoCrud
    {
        ExamenMedicoMapper mapper;
        SqlDao dao;

        public ExamenMedicoCrud()
        { 
            mapper = new ExamenMedicoMapper();
            dao = SqlDao.GetInstance();
        }

        public void Create(ExamenMedico examenMedico)
        {
            SqlOperation operation = mapper.GetCreateStatement(examenMedico);
            dao.ExecuteStoredProcedure(operation);
        }

        public List<ExamenMedico> GetExamenesPaciente(int idPaciente) { 
            List<ExamenMedico> examenesMedicos = new List<ExamenMedico>();  
            SqlOperation operation = mapper.GetRetrieveByPacienteIdOperation(idPaciente);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            examenesMedicos = mapper.BuildObjects(result);
            return examenesMedicos;

            
        }

    }
}

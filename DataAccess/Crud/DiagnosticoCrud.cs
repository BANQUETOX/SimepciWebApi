using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.Diagnosticos;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class DiagnosticoCrud
    {
        public DiagnosticoMapper mapper;
        public SqlDao sqlDao;

        public DiagnosticoCrud()
        {
            mapper = new DiagnosticoMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public void CreateDiagnostico(Diagnostico diagnostico)
        {
            SqlOperation operation = mapper.GetCreateStatement(diagnostico);
            sqlDao.ExecuteStoredProcedure(operation);

        }

        public List<Diagnostico> GetDiagnosticosPaciente(int idPaciente) { 
            List<Diagnostico> diagnosticos = new List<Diagnostico>();
            SqlOperation operation = mapper.GetRetrieveDiagnosticosByPacienteId(idPaciente);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            diagnosticos = mapper.BuildObjects(result);
            return diagnosticos;
            
        }

    }
}

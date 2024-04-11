using DataAccess.Crud;
using DTO.ExamenesMedicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class ExamenMedicoManager
    {
        ExamenMedicoCrud crud = new ExamenMedicoCrud();

        public void CrearExamenMedico(ExamenMedicoInsert examenMedicoInsert)
        {
            ExamenMedico examenMedico = new ExamenMedico();
            examenMedico.idPaciente = examenMedicoInsert.idPaciente;
            examenMedico.idTipoExamenMedico = examenMedicoInsert.idTipoExamenMedico;
            examenMedico.resultado = examenMedicoInsert.resultado;
            crud.Create(examenMedico);
        }

        public List<ExamenMedico> GetExamenMedicosPaciente(int idPaciente)
        {
            return crud.GetExamenesPaciente(idPaciente);
        }
    }
}

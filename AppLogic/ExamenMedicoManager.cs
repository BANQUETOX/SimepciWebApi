using DataAccess.Crud;
using DTO;
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
        PacienteCrud pacienteCrud = new PacienteCrud();

        public string CrearExamenMedico(ExamenMedicoInsert examenMedicoInsert)
        {
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(examenMedicoInsert.idUsuario);
            ExamenMedico examenMedico = new ExamenMedico();
            examenMedico.idPaciente = paciente.Id;
            examenMedico.idTipoExamenMedico = examenMedicoInsert.idTipoExamenMedico;
            examenMedico.resultado = examenMedicoInsert.resultado;
            examenMedico.objetivo = examenMedicoInsert.objetivo;
            crud.Create(examenMedico);
            return "Examen Medico creado";
        }

        public List<ExamenMedico> GetExamenMedicosPaciente(int idPaciente)
        {
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(idPaciente);
            return crud.GetExamenesPaciente(paciente.Id);
        }
    }
}

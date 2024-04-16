using DataAccess.Crud;
using DTO;
using DTO.Expedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class ExpedienteManager
    {
        ExpedienteCrud crud = new ExpedienteCrud();
        CitaManager citaManager = new CitaManager();
        ExamenMedicoManager examenMedicoManager = new ExamenMedicoManager();
        RecetaManager recetaManager = new RecetaManager();
        PacienteCrud pacienteCrud = new PacienteCrud();

        public string CreateExpediente(ExpedienteInput expedienteInput)
        {
            Expediente expediente = new Expediente();
            expediente.idPaciente = expedienteInput.idPaciente;
            expediente.notasEnfermeria = expedienteInput.notasEnfermeria;
            expediente.notasMedicas = expedienteInput.notasMedicas;
            expediente.historialMedico = expedienteInput.historialMedico;
            crud.Create(expediente);
            return "Expediente Creado";
        }

        public Expediente GetExpedietePaciente(int idPaciente)
        {
            return crud.GetExpedietePaciente(idPaciente);
        }

        public ExpedienteCompleto GetExpedienteCompleto(int idUsuario)
        {
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(idUsuario);
            ExpedienteCompleto expedienteCompleto = new ExpedienteCompleto();
            expedienteCompleto.infoExpediente = GetExpedietePaciente(paciente.Id);
            expedienteCompleto.citas = citaManager.CitasPaciente(paciente.Id);
            expedienteCompleto.examenesMedicos = examenMedicoManager.GetExamenMedicosPaciente(paciente.Id);
            expedienteCompleto.recetas = recetaManager.GetRecetasPaciente(paciente.Id);
            return expedienteCompleto;
        }
    }
}

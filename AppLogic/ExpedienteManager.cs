using DataAccess.Crud;
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

        public ExpedienteCompleto GetExpedienteCompleto(int idPaciente)
        {
            ExpedienteCompleto expedienteCompleto = new ExpedienteCompleto();
            expedienteCompleto.infoExpediente = GetExpedietePaciente(idPaciente);
            expedienteCompleto.citas = citaManager.CitasPaciente(idPaciente);
            expedienteCompleto.examenesMedicos = examenMedicoManager.GetExamenMedicosPaciente(idPaciente);
            expedienteCompleto.recetas = recetaManager.GetRecetasPaciente(idPaciente);
            return expedienteCompleto;
        }
    }
}

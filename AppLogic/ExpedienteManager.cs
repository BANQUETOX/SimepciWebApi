using DataAccess.Crud;
using DTO;
using DTO.Expedientes;
using DTO.Usuarios;
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
        FacturaManager facturaManager= new FacturaManager();
        UsuarioCrud usuarioCrud = new UsuarioCrud();    

        public string CreateExpediente(ExpedienteInput expedienteInput)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(expedienteInput.correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            Expediente expediente = new Expediente();
            expediente.idPaciente = paciente.Id;
            expediente.notasEnfermeria = expedienteInput.notasEnfermeria;
            expediente.notasMedicas = expedienteInput.notasMedicas;
            expediente.historialMedico = expedienteInput.historialMedico;
            crud.Create(expediente);
            return "Expediente Creado";
        }

        public string InicializarExpediente(ExpedienteInput expedienteInput)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(expedienteInput.correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            Expediente expediente = new Expediente();
            expediente.idPaciente = paciente.Id;
            expediente.notasEnfermeria = expedienteInput.notasEnfermeria;
            expediente.notasMedicas = expedienteInput.notasMedicas;
            expediente.historialMedico = expedienteInput.historialMedico;
            crud.InitialUpdate(expediente);
            return "Expediente Actualizado";
        }

        public Expediente GetExpedietePaciente(string correoPaciente)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            return crud.GetExpedietePaciente(paciente.Id);
        }

        public ExpedienteCompleto GetExpedienteCompleto(string correoPaciente)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            ExpedienteCompleto expedienteCompleto = new ExpedienteCompleto();
            expedienteCompleto.infoExpediente = GetExpedietePaciente(correoPaciente);
            expedienteCompleto.citas = citaManager.CitasPaciente(correoPaciente);
            expedienteCompleto.examenesMedicos = examenMedicoManager.GetExamenMedicosPaciente(paciente.Id);
            expedienteCompleto.recetas = recetaManager.GetRecetasPaciente(correoPaciente);
            expedienteCompleto.facturas = facturaManager.GetFacturasPaciente(correoPaciente);
            return expedienteCompleto;
        }
    }
}

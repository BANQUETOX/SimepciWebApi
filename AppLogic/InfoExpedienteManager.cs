using DataAccess.Crud;
using DTO;
using DTO.InfosExpediente;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class InfoExpedienteManager
    {
        InfoExpedienteCrud infoExpedienteCrud = new InfoExpedienteCrud();
        UsuarioCrud usuarioCrud = new UsuarioCrud();
        PacienteCrud pacienteCrud = new PacienteCrud();

        public string CreateNotaMedica(InfoExpedienteInput notaMedicaInput)
        {
            try
            {
                InfoExpediente notaMedica = castInfoExpedienteInput(notaMedicaInput);
                infoExpedienteCrud.CreateNotaMedica(notaMedica);
                return "Nota medica creada";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string CreateNotaEnfermeria(InfoExpedienteInput notaEnfermeriaInput)
        {
            try
            {
                InfoExpediente notaEnfermeria = castInfoExpedienteInput(notaEnfermeriaInput);
                infoExpedienteCrud.CreateNotaEnfermeria(notaEnfermeria);
                return "Nota de enfermeria creada";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string CreateHistorialMedico(InfoExpedienteInput historialMedicoInput)
        {
            try
            {
                InfoExpediente historialMedico = castInfoExpedienteInput(historialMedicoInput);
                infoExpedienteCrud.CreateHistorialMedico(historialMedico);
                return "Historial medico creado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public InfoExpediente castInfoExpedienteInput(InfoExpedienteInput input)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(input.correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            InfoExpediente infoExpediente = new InfoExpediente();
            infoExpediente.idPaciente = paciente.Id;
            infoExpediente.contenido = input.contenido;
            infoExpediente.fechaEmision = input.fechaEmision;   
            return infoExpediente;

        }


    }
}

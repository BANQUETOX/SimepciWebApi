using DataAccess.Crud;
using DTO;
using DTO.Diagnosticos;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class DiagnosticoManager
    {
        DiagnosticoCrud diagnosticoCrud = new DiagnosticoCrud();
        PacienteCrud pacienteCrud = new PacienteCrud(); 
        UsuarioCrud usuarioCrud = new UsuarioCrud();

        public string CreateDiagnostico(DiagnosticoInput diagnosticoInput)
        {
            try
            {
                
                diagnosticoCrud.CreateDiagnostico(castDiagnosticoInput(diagnosticoInput));
                return "Diagnostico Creado";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        public List<Diagnostico> GetDiagnosticoPaciente(string correoPaciente)
        {
            try
            {
                Usuario usuario = usuarioCrud.GetUsuarioByEmail(correoPaciente);
                Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
                return diagnosticoCrud.GetDiagnosticosPaciente(paciente.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Diagnostico>();
            }
        }

        public Diagnostico castDiagnosticoInput(DiagnosticoInput input)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(input.correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            Diagnostico diagnostico = new Diagnostico();
            diagnostico.nombre = input.nombre;
            diagnostico.descripcion = input.descripcion;
            diagnostico.fechaEmision = input.fechaEmision;  
            diagnostico.idPaciente = paciente.Id;
            return diagnostico;
        }
    }
}

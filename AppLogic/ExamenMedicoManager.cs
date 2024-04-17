using DataAccess.Crud;
using DTO;
using DTO.ExamenesMedicos;
using DTO.TiposExamenes;
using DTO.Usuarios;
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
        UsuarioCrud usuarioCrud = new UsuarioCrud();
        TipoExamenCrud tipoExamenCrud = new TipoExamenCrud();
        public string CrearExamenMedico(ExamenMedicoInsert examenMedicoInsert)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(examenMedicoInsert.correoUsuario);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            ExamenMedico examenMedico = new ExamenMedico();
            examenMedico.idPaciente = paciente.Id;
            examenMedico.idTipoExamenMedico = examenMedicoInsert.idTipoExamenMedico;
            examenMedico.resultado = examenMedicoInsert.resultado;
            examenMedico.objetivo = examenMedicoInsert.objetivo;
            crud.Create(examenMedico);
            return "Examen Medico creado";
        }

        public List<ExamenMedicoOutput> GetExamenMedicosPaciente(int idPaciente)
        {
            List<ExamenMedicoOutput> listaOutputs = new List<ExamenMedicoOutput>();
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(idPaciente);
            List<ExamenMedico> examenesMedicos =  crud.GetExamenesPaciente(paciente.Id);
            foreach (var examen in examenesMedicos){
                var output = castExamenMedicoOutput(examen);
                listaOutputs.Add(output);
            }
            return listaOutputs;

        }

        public ExamenMedicoOutput GetExamenMedicoById(int idExamenMedico)
        {
            ExamenMedico examenMedico = crud.GetExamenMedicoById(idExamenMedico);
            ExamenMedicoOutput output = castExamenMedicoOutput(examenMedico);
            return output;
        }

        public string UpdateExamenMedico(string resultado, int idExamenMedico)
        {
            string result;
            try
            {
                ExamenMedico examenInicial = crud.GetExamenMedicoById(idExamenMedico);
                ExamenMedico examenActualizado = new ExamenMedico();
                examenActualizado.Id = idExamenMedico;
                examenActualizado.idPaciente = examenInicial.idPaciente;
                examenActualizado.idTipoExamenMedico = examenInicial.idTipoExamenMedico;
                examenActualizado.resultado = resultado;
                examenActualizado.objetivo = examenInicial.objetivo;

                crud.Update(examenActualizado);

                result = "Examen Actualizado";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }

        public string DeleteExamenMedico(int idExamenMedico)
        {
            string result;
            try
            {
                crud.Delete(idExamenMedico);
                result = "Examen Medico eliminado";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public ExamenMedicoOutput castExamenMedicoOutput(ExamenMedico examenMedico) {
            TipoExamen tipoExamenMedico = tipoExamenCrud.GetTipoExamenById(examenMedico.idTipoExamenMedico);
            ExamenMedicoOutput output = new ExamenMedicoOutput();
            output.Id = examenMedico.Id;
            output.nombreTipoExamenMedico = tipoExamenMedico.nombre;
            output.idPaciente = examenMedico.idPaciente;
            output.resultado = examenMedico.resultado;
            output.objetivo = examenMedico.objetivo;
            return output;
        }
    }
}

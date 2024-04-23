using DataAccess.Crud;
using DTO;
using DTO.Recetas;
using DTO.Sedes;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class RecetaManager
    {
        RecetaCrud crud = new RecetaCrud();
        PacienteCrud pacienteCrud = new PacienteCrud();
        UsuarioCrud usuarioCrud = new UsuarioCrud();
        SedeCrud sedeCrud = new SedeCrud();
        DoctorCrud doctorCrud = new DoctorCrud();   

        public string CrearReceta(RecetaInput recetaInput)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(recetaInput.correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            /*Usuario usuarioDoctor = usuarioCrud.GetUsuarioByEmail(recetaInput.correoDoctor);
            Doctor doctor = doctorCrud.GetDoctorByUsuarioId(usuarioDoctor.Id);*/
            Receta receta = new Receta();
            receta.nombreDoctor = recetaInput.nombreDoctor;
            receta.idPaciente = paciente.Id;
            receta.imagen = recetaInput.imagen;
            receta.fechaEmision = DateTime.Now;
            receta.medicamento = recetaInput.medicamento;   
            receta.dosis = recetaInput.dosis;
            receta.diasDosis = recetaInput.diasDosis;
            receta.recomendaciones = recetaInput.recomendaciones;
            crud.Create(receta);
            return "Receta Creada";
        }

        public List<RecetaOutput> GetRecetasPaciente(string correoPaciente) {
            List<RecetaOutput> recetas = new List<RecetaOutput>();
            Usuario usuarioPaciente = usuarioCrud.GetUsuarioByEmail(correoPaciente);
            
            List<Receta> recetasBase = crud.GetRecetasPaciente(usuarioPaciente.Id);
            foreach (var receta in recetasBase)
            {
                RecetaOutput output = castRecetaOutput(receta);
                recetas.Add(output);
            }
            return recetas;
        }

        public string UpdateReceta(Receta receta)
        {
            string result;
            try
            {
                crud.Update(receta);
                result = "Receta Actualizada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string DeleteReceta(int idReceta)
        {
            string result;
            try
            {
                crud.Delete(idReceta);
                result = "Receta Eliminada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public Receta GetRecetaById (int idReceta)
        {
            Receta receta = new Receta();
            try
            {

             receta = crud.GetRecetaById(idReceta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return receta;

        }


        public RecetaOutput castRecetaOutput(Receta recetaBase)
        {
            Usuario usuarioPaciente = usuarioCrud.RetrieveByPacienteId(recetaBase.idPaciente);
            /*Usuario usuarioDoctor = usuarioCrud.RetrieveByDoctorId(recetaBase.idDoctor);
            Doctor doctor = doctorCrud.GetDoctorById(recetaBase.idDoctor);*/
            /*Sede sedeDoctor = sedeCrud.RetrieveById(doctor.idSede);*/
            RecetaOutput recetaOutput = new RecetaOutput();
            recetaOutput.idPaciente = recetaBase.idPaciente;
            /*recetaOutput.idDoctor = recetaBase.idDoctor;*/
            recetaOutput.imagen = recetaBase.imagen;    
            recetaOutput.fechaEmision = recetaBase.fechaEmision;
            recetaOutput.medicamento = recetaBase.medicamento;  
            recetaOutput.dosis = recetaBase.dosis;
            recetaOutput.diasDosis = recetaBase.diasDosis;
            recetaOutput.recomendaciones = recetaBase.recomendaciones;
            recetaOutput.nombrePaciente = $"{usuarioPaciente.nombre} - {usuarioPaciente.primerApellido} - {usuarioPaciente.segundoApellido}";
            recetaOutput.nombreMedico = recetaBase.nombreDoctor;
            recetaOutput.clinica = $"Centro medico Su salud Primero";
            return recetaOutput;
            

        }
    }
}

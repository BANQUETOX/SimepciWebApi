using DataAccess.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Doctores;
using DTO.Usuarios;
using DTO.Sedes;
using DTO.EspecialidadesMedicas;

namespace AppLogic
{
    public class DoctorManager
    {
        DoctorCrud doctorCrud = new DoctorCrud();
        UsuarioCrud usuarioCrud  = new UsuarioCrud();
        SedeCrud sedeCrud = new SedeCrud();
        EspecialidadMedicaCrud especialidadMedicaCrud = new EspecialidadMedicaCrud();   


        public void CrearDoctor(Doctor doctor)
        {
            doctorCrud.Create(doctor);
        }

        public void EliminarDoctor(int idUsuario)
        {
            doctorCrud.Delete(idUsuario);
        }

        public string UpdateHorarioDoctor(int idDoctor, int horario)
        {
           
            if (horario >= 4  || horario == 0) {
                return "Numero de horario invalido";
            }
            try
            {
                doctorCrud.UpdateHorarioDoctor(idDoctor,horario);
                return "Horario actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateSedeDoctor(int idDoctor, int idSede)
        {
            List<Sede> sedes = sedeCrud.RetrieveAll<Sede>();
            List<int> idsSedes = new List<int>();
            foreach (Sede sede in sedes) { 
                idsSedes.Add(sede.Id);
            }
             if (!idsSedes.Contains(idSede))
            {
                return "Numero id de sede invalido";
            }
            try
            {
                doctorCrud.UpdateSedeDoctor(idDoctor, idSede);
                return "Sede actualizada";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<DoctorOutput> GetAllDoctors()
        {
            List<Doctor> doctores = new List<Doctor>();
            List<DoctorOutput> doctoresOutput = new List<DoctorOutput>();
            try
            {
                doctores = doctorCrud.GetAllDoctores();
                foreach (var doctor in doctores)
                {
                    DoctorOutput output = castDoctorOuput(doctor);
                    doctoresOutput.Add(output);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
            return doctoresOutput;
        }

        public DoctorOutput castDoctorOuput(Doctor doctorBase)
        {
            Usuario usuario = usuarioCrud.RetrieveByDoctorId(doctorBase.Id);
            Sede sede = sedeCrud.RetrieveById(doctorBase.idSede);
            EspecialidadMedica especialidad = especialidadMedicaCrud.GetEspecialidadById(doctorBase.idEspecialidad);

            DoctorOutput output = new DoctorOutput();
            output.idDoctor = doctorBase.Id;
            output.idUsuario = doctorBase.idUsuario;    
            output.idEspecialidad = doctorBase.idEspecialidad;
            output.idSede = doctorBase.idSede;
            output.horario = doctorBase.horario;
            output.nombreDoctor = $"{usuario.nombre} - {usuario.primerApellido} - {usuario.segundoApellido}";
            output.nombreSede = $"{sede.nombre}";
            output.nombreEspecialidad = $"{especialidad.nombre}";
            return output;

        }
    }
}

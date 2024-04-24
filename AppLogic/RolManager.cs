using DataAccess.Crud;
using DTO;
using DTO.Doctores;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class RolManager
    {
        RolCrud rolCrud = new RolCrud();
        AdministradorManager administradorManager = new AdministradorManager();
        DoctorManager doctorManager = new DoctorManager();
        DoctorCrud doctorCrud = new DoctorCrud();
        EnfermeroManager enfermeroManager = new EnfermeroManager();
        SecretarioManager secretarioManager = new SecretarioManager();
        PacienteManager pacienteManager = new PacienteManager();
        UsuarioCrud usuarioCrud   = new UsuarioCrud();


       

        public List<Rol> GetAllRols()
        {
            List<Rol> list = rolCrud.RetrieveAll<Rol>();
            return list;
        }

        public string AsignarRolUsuario(string correoUsuario, int idRol)
        {
            int idUsuario;
            try {
                UsuarioCrud usuarioCrud = new UsuarioCrud();
                Usuario usuarioEncontrado = usuarioCrud.GetUsuarioByEmail(correoUsuario);
                idUsuario = usuarioEncontrado.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message;
            }

            if (idRol > 5)
            {
                return "idRol Invalido";
            }
            if (idRol == 1)
            {
                Administrador administradorExistente = administradorManager.GetAdministradorByUsusarioId(idUsuario);
                if (administradorExistente.Id != 0)
                {
                    return "El usuario ya tiene el rol administrador asignado";
                }
                administradorManager.CrearAdministrador(idUsuario);
            }
            else if (idRol == 2)
            {
                return "Rol no asignable en este metodo";
            }
            else if (idRol == 3)
            {
                Secretario secretarioExistente = secretarioManager.GetSecretarioByUsuarioId(idUsuario);
                if (secretarioExistente.Id != 0)
                {
                    return "El usuario ya tiene el rol secretario asignado";
                }
                secretarioManager.CrearSecretario(idUsuario);
            }
            else if (idRol == 4)
            {
                Enfermero enfermeroExistente = enfermeroManager.GetEnfermeroByUsuarioId(idUsuario);
                if (enfermeroExistente.Id != 0)
                {
                    return "El usuario ya tiene el rol enfermero asignado";
                }
                enfermeroManager.CrearEnfermero(idUsuario);
            }
            else if (idRol == 5) 
            {
                Paciente pacienteExistente = pacienteManager.GetPacienteByUsuarioId(idUsuario);
                if (pacienteExistente.Id != 0)
                {
                    return "El usuario ya tiene el rol paciente asignado";
                }
                pacienteManager.CrearPaciente(idUsuario);
            }
            return rolCrud.AsignarRolUsuario(idUsuario, idRol);
        }

        public string RemoverRolUsuario(int idUsuario, int idRol)
        {
            string result;
            try
            {
                if (idRol > 5)
                {
                    return "idRol Invalido";
                }
                if (idRol == 1)
                {
                    administradorManager.EliminarAdministrador(idUsuario);
                }
                else if (idRol == 2)
                {
                    doctorManager.EliminarDoctor(idUsuario);
                }
                else if (idRol == 3)
                {
                    secretarioManager.EliminarSecretario(idUsuario);
                }
                else if (idRol == 4)
                {
                    enfermeroManager.EliminarEnfermero(idUsuario);
                }
                else if (idRol == 5)
                {
                    pacienteManager.EliminarPaciente(idUsuario);
                }
                rolCrud.RemoverRolUsuario(idUsuario, idRol);
                 result = "El rol a sido removido";

            }
            catch (Exception e)
            {
               result = e.Message;
            }
            return result;
            
        }

        public List<string> GetRolesUsuario(string correo)
        {
            List<string> roles = rolCrud.GetRolesUsuario(correo);
            return roles;
        }

        public string AsignarRolDoctor(DoctorInsert doctorInsert)
        {
            string result;
            try {


                Usuario usuario = usuarioCrud.GetUsuarioByEmail(doctorInsert.correoUsuario);
                Doctor doctorExistente = doctorCrud.GetDoctorByUsuarioId(usuario.Id);
                if (doctorExistente != null || doctorExistente.Id == 0) {
                    return "El usuario ya es doctor";
                }

                Doctor doctor = new Doctor();
                doctor.idUsuario = usuario.Id;
                doctor.idEspecialidad = doctorInsert.idEspecialidad;
                doctor.horario = doctorInsert.horario;
                doctor.idSede = doctorInsert.idSede;
                doctorManager.CrearDoctor(doctor);
                rolCrud.AsignarRolUsuario(usuario.Id, 2);
                result = "Rol doctor asignado";
            }
            catch (Exception e)
            {
                result = e.Message; 
            }
            return result;
           
        }
        
    }
}

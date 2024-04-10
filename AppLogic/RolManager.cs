using DataAccess.Crud;
using DTO;
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
        EnfermeroManager enfermeroManager = new EnfermeroManager();
        SecretarioManager secretarioManager = new SecretarioManager();
        PacienteManager pacienteManager = new PacienteManager();


       

        public List<Rol> GetAllRols()
        {
            List<Rol> list = rolCrud.RetrieveAll<Rol>();
            return list;
        }

        public string AsignarRolUsuario(Usuario usuario, int idRol)
        {
            if (idRol > 5)
            {
                return "idRol Invalido";
            }
            if (idRol == 1)
            {
                administradorManager.CrearAdministrador(usuario.Id);
            }
            else if (idRol == 2)
            {
                doctorManager.CrearDoctor(usuario.Id);
            }
            else if (idRol == 3)
            {
                secretarioManager.CrearSecretario(usuario.Id);
            }
            else if (idRol == 4)
            {
                enfermeroManager.CrearEnfermero(usuario.Id);
            }
            else if (idRol == 5) 
            {
                pacienteManager.CrearPaciente(usuario.Id);
            }
            return rolCrud.AsignarRolUsuario(usuario.Id, idRol);
        }

        public string RemoverRolUsuario(Usuario usuario, int idRol)
        {
            if (idRol > 5)
            {
                return "idRol Invalido";
            }
            if (idRol == 1)
            {
                administradorManager.EliminarAdministrador(usuario.Id);
            }
            else if (idRol == 2)
            {
                doctorManager.EliminarDoctor(usuario.Id);
            }
            else if (idRol == 3)
            {
                secretarioManager.EliminarSecretario(usuario.Id);
            }
            else if (idRol == 4)
            {
                enfermeroManager.EliminarEnfermero(usuario.Id);
            }
            else if (idRol == 5) 
            {
                pacienteManager.EliminarPaciente(usuario.Id);
            }
            return rolCrud.RemoverRolUsuario(usuario.Id, idRol);
        }

        public List<string> GetRolesUsuario(string correo)
        {
            List<string> roles = rolCrud.GetRolesUsuario(correo);
            return roles;
        }
    }
}

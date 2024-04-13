﻿using DataAccess.Crud;
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
                administradorManager.CrearAdministrador(idUsuario);
            }
            else if (idRol == 2)
            {
                return "Rol no asignable en este metodo";
            }
            else if (idRol == 3)
            {
                secretarioManager.CrearSecretario(idUsuario);
            }
            else if (idRol == 4)
            {
                enfermeroManager.CrearEnfermero(idUsuario);
            }
            else if (idRol == 5) 
            {
                pacienteManager.CrearPaciente(idUsuario);
            }
            return rolCrud.AsignarRolUsuario(idUsuario, idRol);
        }

        public string RemoverRolUsuario(int idUsuario, int idRol)
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
            return rolCrud.RemoverRolUsuario(idUsuario, idRol);
        }

        public List<string> GetRolesUsuario(string correo)
        {
            List<string> roles = rolCrud.GetRolesUsuario(correo);
            return roles;
        }

        public void AsignarRolDoctor(DoctorInsert doctorInsert)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(doctorInsert.correoUsuario);
            Doctor doctor = new Doctor();
            doctor.idUsuario = usuario.Id;  
            doctor.idEspecialidad = doctorInsert.idEspecialidad;
            doctor.horario = doctorInsert.horario;  
            doctor.idSede = doctorInsert.idSede;
            doctorManager.CrearDoctor(doctor);
            rolCrud.AsignarRolUsuario(usuario.Id, 2);
        }
        
    }
}

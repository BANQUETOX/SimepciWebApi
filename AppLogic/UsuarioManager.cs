using Azure.Core;
using DataAccess.Crud;
using DTO;
using DTO.Expedientes;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppLogic
{
    public class UsuarioManager
    {
        UsuarioCrud usuarioCrud = new UsuarioCrud();
        RolManager rolManager = new RolManager();
        ExpedienteManager expedienteManager = new ExpedienteManager();
        PacienteManager pacienteManager = new PacienteManager();
        EmailManager emailManager = new EmailManager();


        public string CreateUsuario(Usuario usuario, bool esFuncionario)
        {

            Usuario usuarioExistente = usuarioCrud.RetrieveByCedula(usuario.cedula);
            if (usuarioExistente.Id != 0) {
                return "La cedula ya ha sido registrada";
            }
            
            string correo = usuario.correo;
            if (!verificarCorreo(correo))
            {
                return "Formato de correo invalido";
            }
            else if (GetUsuarioByEmail(correo) != null) {
                return "El correo ya ha sido registrado";
            }

            usuarioCrud.Create(usuario);

            Usuario fullUsuario = usuarioCrud.GetUsuarioByEmail(usuario.correo);
            rolManager.AsignarRolUsuario(fullUsuario.correo, 5);
            Paciente paciente = pacienteManager.GetPacienteByUsuarioId(fullUsuario.Id);

            if (esFuncionario)
            {
                _ = emailManager.SendSolicitudFuncionario(fullUsuario.correo);
            }
            return "Usuario creado";
        }

        public List<UsuarioGet> GetAllUsuarios()
        {
            List<Usuario> list = usuarioCrud.RetrieveAll();
            List<UsuarioGet> listResult = new List<UsuarioGet>();
            foreach (Usuario usuario in list)
            {
                UsuarioGet usuarioGet = castUsuarioGet(usuario);
                listResult.Add(usuarioGet);

            }
            return listResult;
        }

        public Usuario GetUsuarioByEmail(string correo)
        {

            Usuario usuario = usuarioCrud.GetUsuarioByEmail(correo);
            return usuario;
        }

        public UsuarioGet GetUsuarioById(int id) {
            Usuario usuario = usuarioCrud.RetrieveById(id);
            UsuarioGet usuarioGet = castUsuarioGet(usuario);
            return usuarioGet;
        }


        public UsuarioGet Login(string correo, string password)
        {
            UsuarioGet result;

            try
            {
                Usuario usuario = usuarioCrud.Login(correo, password);
                result = castUsuarioGet(usuario);
            }
            catch (Exception ex)
            {
                result = new UsuarioGet();
            }

            return result;


        }

        public string actualizarPassword(string correoUsuario, string newPassword)
        {
            if (GetUsuarioByEmail(correoUsuario) == null)
            {
                return "Usuario inexistente";
            }
            usuarioCrud.UpdatePassword(correoUsuario, newPassword);
            return "Dato actualizado exitosamente";
        }

        public bool verificarCorreo(string correo)
        {
            string patronCorreo = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(correo, patronCorreo);
        }


        public string desactivarUsuario(string correo)
        {
            string resultado = usuarioCrud.DesactivarUsuario(correo);
            return resultado;
        }

        public string activarUsuario(string correo)
        {
            string resultado = usuarioCrud.ActivarUsuario(correo);
            return resultado;
        }

        public string UpdateUsuario(UsuarioUpdate usuario)
        {
            string result;
            try
            {
                usuarioCrud.Update(usuario);
                result = "Usuario Actualizado";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;

        }

       
        public Usuario castUsuarioInsert(UsuarioInsert usuarioInsert)
        {
            Usuario usuario = new Usuario();
            usuario.nombre = usuarioInsert.nombre;
            usuario.primerApellido = usuarioInsert.primerApellido;
            usuario.segundoApellido = usuarioInsert.segundoApellido;
            usuario.cedula = usuarioInsert.cedula;
            usuario.fechaNacimiento = usuarioInsert.fechaNacimiento;
            usuario.telefono = usuarioInsert.telefono;
            usuario.correo = usuarioInsert.correo;
            usuario.direccion = usuarioInsert.direccion;
            usuario.fotoPerfil = usuarioInsert.fotoPerfil;
            usuario.activo = false;
            usuario.sexo = usuarioInsert.sexo;
            usuario.password = usuarioInsert.password;
            return usuario;

        }

        public UsuarioGet castUsuarioGet(Usuario usuarioBase) { 
            UsuarioGet usuario = new UsuarioGet();
            usuario.Id = usuarioBase.Id;
            usuario.nombre = usuarioBase.nombre;
            usuario.primerApellido = usuarioBase.primerApellido;
            usuario.segundoApellido = usuarioBase.segundoApellido;
            usuario.cedula = usuarioBase.cedula;
            usuario.fechaNacimiento = usuarioBase.fechaNacimiento;
            usuario.edad = usuarioBase.edad;
            usuario.telefono = usuarioBase.telefono;
            usuario.correo = usuarioBase.correo;
            usuario.direccion = usuarioBase.direccion;
            usuario.fotoPerfil = usuarioBase.fotoPerfil;
            usuario.activo = usuarioBase.activo;
            usuario.sexo = usuarioBase.sexo;
            usuario.roles = rolManager.GetRolesUsuario(usuarioBase.correo);
            return usuario;
        }

        public Usuario castUsuarioUpdate(UsuarioUpdate usuarioUpdate)
        {
            Usuario usuario = new Usuario();
            usuario.nombre = usuarioUpdate.nombre;
            usuario.primerApellido = usuarioUpdate.primerApellido;
            usuario.segundoApellido = usuarioUpdate.segundoApellido;
            usuario.cedula = usuarioUpdate.cedula;
            usuario.fechaNacimiento = usuarioUpdate.fechaNacimiento;
            usuario.telefono = usuarioUpdate.telefono;
            usuario.correo = usuarioUpdate.correo;
            usuario.direccion = usuarioUpdate.direccion;
            usuario.fotoPerfil = usuarioUpdate.fotoPerfil;
            usuario.sexo = usuarioUpdate.sexo;
            return usuario;

        }
    }
}

using DataAccess.Crud;
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

        public string CreateUsuario(Usuario usuario) 
        {
            string correo = usuario.correo;
            if (!verificarCorreo(correo))
            {
                return "Formato de correo invalido";
            }
            else if (GetUsuarioByEmail(correo) != null ) {
                return "El correo ya ha sido registrado";
            }
           
            usuarioCrud.Create(usuario);
            return "Usuario creado";
        }

        public List<UsuarioGet> GetAllUsuarios()
        {
            List<Usuario> list = usuarioCrud.RetrieveAll<Usuario>();
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
            return  usuario;
        }


        public Usuario Login(string correo, string password)
        {
            Usuario usuario = usuarioCrud.Login(correo, password);
            return usuario; 
            

        }

        public void actualizarPassword(string correoUsuario, string newPassword)
        {
            usuarioCrud.UpdatePassword(correoUsuario,newPassword);
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

        public List<string> GetRolesUsuario(string correo)
        {
            List<string> roles = usuarioCrud.GetRolesUsuario(correo);
            return roles;
        }

        public string AsignarRolUsuario(string correoUsuario,int idRol)
        {
            Usuario usuario = GetUsuarioByEmail(correoUsuario);
            return usuarioCrud.AsignarRolUsuario(usuario.Id,idRol);
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
            usuario.activo = true;
            usuario.sexo = usuarioInsert.sexo;
            usuario.password = usuarioInsert.password;
            return usuario;

        }

        public UsuarioGet castUsuarioGet(Usuario usuarioBase) { 
            UsuarioGet usuario = new UsuarioGet();
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
            return usuario;
        }
    }
}

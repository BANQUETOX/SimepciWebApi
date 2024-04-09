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
        UsuarioManager usuarioManager = new UsuarioManager();

       

        public List<Rol> GetAllRols()
        {
            List<Rol> list = rolCrud.RetrieveAll<Rol>();
            return list;
        }

        public string AsignarRolUsuario(string correoUsuario, int idRol)
        {
            Usuario usuario = usuarioManager.GetUsuarioByEmail(correoUsuario);
            return rolCrud.AsignarRolUsuario(usuario.Id, idRol);
        }

        public string RemoverRolUsuario(string correoUsuario, int idRol)
        {
            Usuario usuario = usuarioManager.GetUsuarioByEmail(correoUsuario);
            return rolCrud.RemoverRolUsuario(usuario.Id, idRol);
        }

        public List<string> GetRolesUsuario(string correo)
        {
            List<string> roles = rolCrud.GetRolesUsuario(correo);
            return roles;
        }
    }
}

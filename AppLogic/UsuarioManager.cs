using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class UsuarioManager
    {
        UsuarioCrud usuarioCrud = new UsuarioCrud();



        public string CreateUsuario(Usuario usuario)
        {
            usuarioCrud.Create(usuario);
            return "Usuario creado";
        }

        public List<Usuario> GetAllUsuarios()
        {
            List<Usuario> list = usuarioCrud.RetrieveAll<Usuario>();
            return list;
        }

        public void actualizarPassword(string correoUsuario, string newPassword)
        {
            usuarioCrud.UpdatePassword(correoUsuario,newPassword);
        }
    }
}

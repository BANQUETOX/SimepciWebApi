using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class AdministradorManager
    {
        AdministradorCrud administradorCrud = new AdministradorCrud();


        public void CrearAdministrador(int idUsuario)
        {
            administradorCrud.Create(idUsuario);
        }

        public void EliminarAdministrador(int idUsuario)
        {
            administradorCrud.Delete(idUsuario);
        }
        public Administrador GetAdministradorByUsusarioId(int idUsuario)
        {
            return administradorCrud.GetAdministradorByUsuarioId(idUsuario);
        }
    }
}

using DataAccess.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class EnfermeroManager
    {
        EnfermeroCrud enfermeroCrud = new EnfermeroCrud();


        public void CrearEnfermero(int idUsuario)
        {
            enfermeroCrud.Create(idUsuario);
        }

        public void EliminarEnfermero(int idUsuario)
        {
            enfermeroCrud.Delete(idUsuario);
        }
    }
}

using DataAccess.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class DoctorManager
    {
        DoctorCrud doctorCrud = new DoctorCrud();


        public void CrearDoctor(int idUsuario)
        {
            doctorCrud.Create(idUsuario);
        }

        public void EliminarDoctor(int idUsuario)
        {
            doctorCrud.Delete(idUsuario);
        }
    }
}

using DataAccess.Crud;
using DTO;
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

       

        public List<Rol> GetAllRols()
        {
            List<Rol> list = rolCrud.RetrieveAll<Rol>();
            return list;
        }
    }
}

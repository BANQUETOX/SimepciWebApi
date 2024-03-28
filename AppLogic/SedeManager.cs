using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class SedeManager
    {
        SedeCrud sedeCrud = new SedeCrud();


        public List<Rol> GetAllRols()
        {
            List<Rol> list = sedeCrud.RetrieveAll<Rol>();
            return list;
        }
    }
}

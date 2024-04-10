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


        public List<Sede> GetAllSedes()
        {
            List<Sede> list = sedeCrud.RetrieveAll<Sede>();
            return list;
        }

        public string CrearSede(Sede sede)
        {
            string result;
            try
            {
                sedeCrud.Create(sede);
                result = "Sede creada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
            
        }
    }
}

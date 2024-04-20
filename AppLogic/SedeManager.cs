using Azure.Core;
using DataAccess.Crud;
using DTO.Sedes;
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

        public string CrearSede(SedeInsert sedeInsert)
        {
            string result;
            try
            {

                sedeCrud.Create(CastSedeInsert(sedeInsert));
                result = "Sede creada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
            
        }

        public Sede GetSedeById (int idsede)
        {
            Sede sede = new Sede();
            try
            {
                sede = sedeCrud.RetrieveById(idsede);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sede;
        }

        public string UpdateSede(Sede sedeUpdate)
        {
            string result;
            try
            {
                sedeCrud.Update(sedeUpdate);
                result = "Sede actualizada";
            }
            catch (Exception ex) { 
                result = ex.Message;
            }
            return result;
        }

        public string DeleteSede (int idsede)
        {
            string result;
            try
            {
                sedeCrud.Delete(idsede);
                result = "Sede eliminada";
            }
            catch(Exception ex)
            {
                result = ex.Message;    
            }
            return result;
        }


        public Sede CastSedeInsert(SedeInsert sedeInsert)
        {
            Sede sede = new Sede();
            sede.nombre = sedeInsert.nombre;
            sede.descripcion = sedeInsert.descripcion;
            sede.fechaCreacion = sedeInsert.fechaCreacion;
            sede.ubicacion = sedeInsert.ubicacion;
            sede.foto = sedeInsert.foto;
            sede.provincia = sedeInsert.provincia;
            sede.canton = sedeInsert.canton;
            sede.distrito = sedeInsert.distrito;    
            return sede;
        }
    }
}

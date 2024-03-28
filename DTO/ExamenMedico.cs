using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ExamenMedico : BaseClass
    {
        public int idTipoExamenMedico { get; set; }
        public string resultado { get; set; }   // De tipo image en la base de datos,falta arregar el tipo que se maneja en el dto

    }
}

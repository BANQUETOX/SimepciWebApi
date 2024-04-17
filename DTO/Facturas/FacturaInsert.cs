using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Facturas
{
    public class FacturaInsert
    {
        public int idCita {  get; set; }
        public List<CostoAdicional> costosAdicionales { get; set; }
    }
}

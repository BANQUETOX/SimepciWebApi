using DTO.CostosAdicionales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Facturas
{
    public class FacturaInput
    {
        public int idCita { get; set; }
        public List<CostoAdicionalInsert> costosAdicionales { get; set; }
    }
}

using DTO.CostosAdicionales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Facturas
{
    public class FacturaCompleta : BaseClass
    {
        public int idCita { get; set; }
        public float monto { get; set; }
        public DateTime fechaEmision { get; set; }
        public bool pagada { get; set; }
        public List<CostoAdicional> costosAdicionales { get; set; }
    }
}

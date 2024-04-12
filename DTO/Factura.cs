using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Factura : BaseClass
    {
        public int idCita {  get; set; }
        public float monto {  get; set; }
        public DateTime fechaEmision {  get; set; }
        public bool pagada { get; set; }
    }
}

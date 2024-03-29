using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Factura : BaseClass
    {
        public int monto {  get; set; }
        public DateTime fechaEmision {  get; set; }
        public int pagada { get; set; }
    }
}

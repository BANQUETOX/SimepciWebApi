using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CostosAdicionales
{
    public class CostoAdicional : BaseClass
    {
        public int idFactura { get; set; }
        public string nombre { get; set; }
        public float precio { get; set; }
    }
}

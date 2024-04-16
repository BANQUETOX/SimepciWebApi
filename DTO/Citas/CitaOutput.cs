using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Citas
{
    public class CitaOutput
    {
        public DateTime fecha { get; set; }
        public string especialidad { get; set; }
        public string doctor { get; set; }
        public string precio { get; set; }
    }
}

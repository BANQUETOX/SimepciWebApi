using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Sedes
{
    public class SedeInsert
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string ubicacion { get; set; }
        public string foto { get; set; }
        public string provincia { get; set; }
        public string canton { get; set; }
        public string distrito { get; set; }
    }
}

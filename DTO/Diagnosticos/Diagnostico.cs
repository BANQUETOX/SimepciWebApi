using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Diagnosticos
{
    public class Diagnostico : BaseClass
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaEmision { get; set; }
        public int idPaciente { get; set; }

    }
}

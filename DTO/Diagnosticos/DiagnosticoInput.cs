using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Diagnosticos
{
    public class DiagnosticoInput
    {
        public string correoPaciente { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaEmision { get; set; }
    }
}

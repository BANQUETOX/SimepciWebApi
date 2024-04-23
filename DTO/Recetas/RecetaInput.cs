using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Recetas
{
    public class RecetaInput
    {
        public string correoPaciente { get; set; }
        public string nombreDoctor {  get; set; } 
        public string imagen { get; set; }
        public string medicamento { get; set; }
        public string dosis { get; set; }
        public string diasDosis { get; set; }
        public string recomendaciones { get; set; }
    }
}

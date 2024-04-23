using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Recetas
{
    public class RecetaOutput
    {
        public int idPaciente { get; set; }
        /*public int idDoctor { get; set; }*/
        public string nombrePaciente { get; set; }
        public string clinica { get; set; }
        public string nombreMedico { get; set; }    
        public string imagen { get; set; }
        public DateTime fechaEmision { get; set; }
        public string medicamento { get; set; }
        public string dosis { get; set; }
        public string diasDosis { get; set; }
        public string recomendaciones { get; set; }
    }
}

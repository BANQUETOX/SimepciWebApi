using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Citas
{
    public class CitaOutputDoctor : BaseClass
    {
        public int idPaciente { get; set; }
        public string nombrePaciente {  set; get; }
        public int idDoctor { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFinal { get; set; }
        public int idSede { get; set; }
    }
}

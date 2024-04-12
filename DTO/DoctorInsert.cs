using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DoctorInsert
    {
        public string correoUsuario { get; set; }
        public int idEspecialidad { get; set; }
        public int idSede { get; set; }
        public int horario { get; set; }
    }
}

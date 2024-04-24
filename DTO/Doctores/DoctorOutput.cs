using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Doctores
{
    public class DoctorOutput
    {
        public string nombreDoctor {  get; set; }
        public string nombreSede {  get; set; }
        public string nombreEspecialidad {  get; set; }

        public  int idDoctor { get; set; }
        public int idUsuario { get; set; }
        public int idEspecialidad { get; set; }
        public int idSede { get; set; }
        public int horario { get; set; }
    }
}

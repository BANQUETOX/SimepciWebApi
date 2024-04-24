using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Doctores
{
    public class Doctor : BaseClass
    {
        public int idUsuario { get; set; }
        public int idEspecialidad { get; set; }
        public int idSede { get; set; }
        public int horario { get; set; }
    }
}

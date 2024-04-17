using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Citas
{
    public class CitaInsert
    {
        public int idUsuarioPaciente { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFinal { get; set; }
        public int idSede { get; set; }

        public int idEspecialidad {  get; set; }
    }
}

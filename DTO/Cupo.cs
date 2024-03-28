using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Cupo : BaseClass
    {
        public DateTime horaInicio {  get; set; }
        public DateTime horaFinal { get; set; }
        public int idCita { get; set; }
        public int idEspecialidadMedica { get; set; }

    }
}

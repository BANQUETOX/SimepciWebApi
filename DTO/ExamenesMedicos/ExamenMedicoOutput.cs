using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ExamenesMedicos
{
    public class ExamenMedicoOutput : BaseClass
    {
        public string nombreTipoExamenMedico { get; set; }
        public int idPaciente { get; set; }
        public string resultado { get; set; }

        public string objetivo { get; set; }
    }
}

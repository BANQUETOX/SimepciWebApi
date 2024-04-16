using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ExamenesMedicos
{
    public class ExamenMedico : BaseClass
    {
        public int idTipoExamenMedico { get; set; }
        public int idPaciente { get; set; }
        public string resultado { get; set; }

        public string objetivo { get; set; }

    }
}

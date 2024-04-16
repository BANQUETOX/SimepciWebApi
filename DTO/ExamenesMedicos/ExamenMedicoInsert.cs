using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ExamenesMedicos
{
    public class ExamenMedicoInsert
    {
        public int idTipoExamenMedico { get; set; }
        public string correoUsuario { get; set; }
        public string resultado { get; set; }
        public string objetivo { get; set; }   
    }
}

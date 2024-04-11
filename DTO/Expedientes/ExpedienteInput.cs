using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Expedientes
{
    public class ExpedienteInput
    {
        public int idPaciente { get; set; }
        public string notasEnfermeria { get; set; }
        public string notasMedicas { get; set; }
        public string historialMedico { get; set; }
    }
}

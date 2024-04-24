using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.InfosExpediente
{
    public class InfoExpedienteInput
    {
        public string correoPaciente { get; set; }
        public string contenido { get; set; }
        public DateTime fechaEmision { get; set; }
    }
}

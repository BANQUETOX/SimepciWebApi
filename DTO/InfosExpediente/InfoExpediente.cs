using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.InfosExpediente
{
    public class InfoExpediente : BaseClass
    {
        public int idPaciente { get; set; }
        public string contenido { get; set; }
        public DateTime fechaEmision { get; set; }

    }
}

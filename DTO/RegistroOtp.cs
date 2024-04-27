using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RegistroOtp : BaseClass
    {
        public string correoUsuario {  get; set; }
        public string codigoOtp { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}

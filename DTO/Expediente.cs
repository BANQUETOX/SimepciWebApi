using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Expediente : BaseClass
    {
        public int idUsuario {  get; set; }
        public string NotasEnfermeria { get; set; }
        public string NotasMedicas { get; set; }
        public string HistorialMedico {  get; set; }
    }
}

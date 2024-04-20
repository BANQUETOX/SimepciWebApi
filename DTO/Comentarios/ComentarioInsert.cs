using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Comentarios
{
    public class ComentarioInsert
    {
        public int satisfaccion { get; set; }
        public int profecionalismo { get; set; }
        public int instalaciones { get; set; }
        public bool recomendaria { get; set; }
        public string comentarios { get; set; }
    }
}

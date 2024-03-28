using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class User : BaseClass
    {
        public string nombre {  get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public int cedula { get; set; }
        public DateOnly fechaNacimieto { get; set; }
        public int edad {  get; set; }
        public int telefono { get; set; }
        public string correo { get; set; }

        public string direccion {  get; set; }
        public string fotoPerfil { get; set; }
        public int activo { get; set; }
        public int especialidadMedica { get; set; }
    }
}

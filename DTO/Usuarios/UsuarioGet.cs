using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Usuarios
{
    public class UsuarioGet
    {
        public string nombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string cedula { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int edad { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }

        public string direccion { get; set; }
        public string fotoPerfil { get; set; }
        public bool activo { get; set; }

        public string sexo { get; set; }

        public List<string> roles { get; set; }


    }
}

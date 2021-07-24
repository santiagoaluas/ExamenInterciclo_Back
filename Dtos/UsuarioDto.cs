using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenInterciclo_Back.Dtos
{
    public class UsuarioDto
    {
        public string username { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string password { get; set; }
        public string foto { get; set; }
        public DateTime fechNacimiento { get; set;}

    }
}

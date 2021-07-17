using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenInterciclo_Back.Dtos
{
    public class Respuesta
    {
        public bool exito { get; set; }
        public string mensaje { get; set; }
        public object data { get; set; }
    }
}

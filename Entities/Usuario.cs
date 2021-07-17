﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenInterciclo_Back.Entities
{
    public class Usuario
    {
        public string id { get; set; }
        public string username { get; set;}
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string password { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public DateTime fecha_registro { get; set; }
    }
}
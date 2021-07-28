using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenInterciclo_Back.Entities
{
    public class Productos
    {
        [Key]
        public string id { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
        public string codigoVenta { get; set; }
        public Double precioUnitario { get; set; }
        public string foto { get; set; }

    }
}

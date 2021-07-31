using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenInterciclo_Back.Interfaces
{
    public interface IEmail 
    {
        public string enviarEmailAuth(string url, string destinoEmail);
        public string enviarPagosEmail(string destinoEmail, string mensaje);

    }
}

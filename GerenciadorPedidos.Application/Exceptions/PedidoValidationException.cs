using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.Exceptions
{
    public class PedidoValidationException : Exception
    {
        public PedidoValidationException(string message) : base(message) { }
    }
}

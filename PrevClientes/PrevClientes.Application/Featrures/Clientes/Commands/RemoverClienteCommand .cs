using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevClientes.Application.Featrures.Clientes.Commands
{
    public class RemoverClienteCommand : IRequest<bool>
    {
        public int ClienteId { get; set; }
    }
}

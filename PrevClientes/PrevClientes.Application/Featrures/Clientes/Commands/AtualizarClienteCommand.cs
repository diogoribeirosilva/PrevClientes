using MediatR;
using PrevClientes.Application.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevClientes.Application.Featrures.Clientes.Commands
{
    public class AtualizarClienteCommand : IRequest<bool>
    {
        public int ClienteId { get; set; }
        public ClienteDTO ClienteDTO { get; set; }
    }
}

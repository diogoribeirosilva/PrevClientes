using MediatR;
using PrevClientes.Application.DTO.DTO;

namespace PrevClientes.Application.Features.Clientes.Commands
{
    public class CriarClienteCommand : IRequest<int>
    {
        public ClienteDTO Cliente { get; set; }

        public CriarClienteCommand(ClienteDTO cliente)
        {
            Cliente = cliente;
        }
    }
}

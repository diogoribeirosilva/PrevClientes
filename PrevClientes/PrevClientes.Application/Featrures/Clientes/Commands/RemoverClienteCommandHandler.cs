using MediatR;
using PrevClientes.Application.Featrures.Clientes.Commands;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PrevClientes.Application.Features.Clientes.Commands
{
    public class RemoverClienteCommandHandler : IRequestHandler<RemoverClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public RemoverClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(RemoverClienteCommand command, CancellationToken cancellationToken)
        {
            await _clienteRepository.RemoverCliente(command.ClienteId);

            return true;
        }
    }
}

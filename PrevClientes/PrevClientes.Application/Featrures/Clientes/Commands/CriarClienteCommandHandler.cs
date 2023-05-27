using MediatR;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using PrevClientes.Domain.Models;

namespace PrevClientes.Application.Features.Clientes.Commands
{
    public class CriarClienteCommandHandler : IRequestHandler<CriarClienteCommand, int>
    {
        private readonly IClienteRepository _clienteRepository;

        public CriarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<int> Handle(CriarClienteCommand command, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(
                command.Cliente.Nome,
                command.Cliente.Cpf,
                command.Cliente.Email,
                command.Cliente.Idade,
                command.Cliente.DataNascimento
            );

            var clienteId = await _clienteRepository.CriarCliente(cliente);

            return clienteId;
        }
    }
}

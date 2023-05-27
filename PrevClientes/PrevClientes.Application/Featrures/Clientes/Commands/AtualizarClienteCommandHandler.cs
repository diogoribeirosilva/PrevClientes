using MediatR;
using PrevClientes.Application.Featrures.Clientes.Commands;
using PrevClientes.Domain.Core.Interfaces.Repositories;

namespace PrevClientes.Application.Features.Clientes.Commands
{
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public AtualizarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(AtualizarClienteCommand command, CancellationToken cancellationToken)
        {
            // Obter o cliente existente a partir do ID
            var clienteExistente = await _clienteRepository.ObterClientePorId(command.ClienteId);

            if (clienteExistente == null)
            {
                // Cliente não encontrado
                return false;
            }

            // Atualizar os dados do cliente com base no ClienteDTO fornecido
            clienteExistente.AtualizarDados(
                command.ClienteDTO.Nome,
                command.ClienteDTO.Cpf,
                command.ClienteDTO.Email,
                command.ClienteDTO.Idade,
                command.ClienteDTO.DataNascimento
            );

            // Salvar as alterações no repositório
            await _clienteRepository.AtualizarCliente(clienteExistente);

            return true;
        }
    }
}

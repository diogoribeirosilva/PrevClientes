using FluentValidation;
using MediatR;
using PrevClientes.Application.Featrures.Clientes.Commands;
using PrevClientes.Application.Features.Clientes.Commands;
using PrevClientes.Domain.Core.Interfaces.Repositories;

namespace PrevClientes.Application.Features.Clientes.Commands
{
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<AtualizarClienteCommand> _validator;

        public AtualizarClienteCommandHandler(IClienteRepository clienteRepository, IValidator<AtualizarClienteCommand> validator)
        {
            _clienteRepository = clienteRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(AtualizarClienteCommand command, CancellationToken cancellationToken)
        {
            // Validação do comando
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                // Retornar erros de validação
                throw new ValidationException(validationResult.Errors);
            }

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

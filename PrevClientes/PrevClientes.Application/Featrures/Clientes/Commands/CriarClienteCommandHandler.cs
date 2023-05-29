using FluentValidation;
using MediatR;
using PrevClientes.Application.Featrures.Clientes;
using PrevClientes.Application.Featrures.Clientes.Validator;
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
            var validator = new CriarClienteCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                // Lançar uma exceção ou lidar com os erros de validação de acordo com o seu cenário
                throw new ValidationException(validationResult.Errors);
            }

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

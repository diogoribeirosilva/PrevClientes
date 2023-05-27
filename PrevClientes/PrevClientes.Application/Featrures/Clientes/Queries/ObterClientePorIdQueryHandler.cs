using MediatR;
using PrevClientes.Application.DTO.DTO;
using PrevClientes.Application.Queries.Clientes;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PrevClientes.Application.Features.Clientes.Queries
{
    public class ObterClientePorIdQueryHandler : IRequestHandler<ObterClientePorIdQuery, ClienteDTO>
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClientePorIdQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO> Handle(ObterClientePorIdQuery query, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterClientePorId(query.ClienteId);

            if (cliente == null)
            {
                // Cliente não encontrado
                return null;
            }

            // Mapear o Cliente para ClienteDTO
            var clienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Idade = cliente.Idade,
                DataNascimento = cliente.DataNascimento
            };

            return clienteDTO;
        }
    }
}

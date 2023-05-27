using MediatR;
using PrevClientes.Application.DTO.DTO;
using PrevClientes.Application.Features.Clientes.Queries;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PrevClientes.Application.Features.Clientes.Handlers
{
    public class ObterTodosClientesQueryHandler : IRequestHandler<ObterTodosClientesQuery, List<ClienteDTO>>
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterTodosClientesQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<ClienteDTO>> Handle(ObterTodosClientesQuery request, CancellationToken cancellationToken)
        {

            var clientes = await _clienteRepository.ObterTodosClientes();

            var clientesDTO = new List<ClienteDTO>();

            foreach (var cliente in clientes)
            {
                var clienteDTO = new ClienteDTO
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Cpf = cliente.Cpf,
                    Email = cliente.Email,
                    Idade = cliente.Idade,
                    DataNascimento = cliente.DataNascimento
                };

                clientesDTO.Add(clienteDTO);
            }

            return clientesDTO;
        }
    }
}

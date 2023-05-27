using MediatR;
using PrevClientes.Application.DTO.DTO;
using System.Collections.Generic;

namespace PrevClientes.Application.Features.Clientes.Queries
{
    public class ObterTodosClientesQuery : IRequest<List<ClienteDTO>>
    {
    }
}

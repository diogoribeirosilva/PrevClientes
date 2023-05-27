using PrevClientes.Application.DTO.DTO;
using PrevClientes.Application.Features.Clientes.Queries;

namespace PrevClientes.Application.Features.Clientes.Handlers
{
    public interface IObterTodosClientesQueryHandler
    {
        Task<List<ClienteDTO>> Handle(ObterTodosClientesQuery request, CancellationToken cancellationToken);
    }
}
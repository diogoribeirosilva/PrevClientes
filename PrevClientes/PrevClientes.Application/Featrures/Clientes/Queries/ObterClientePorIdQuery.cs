using MediatR;
using PrevClientes.Application.DTO.DTO;

namespace PrevClientes.Application.Queries.Clientes
{
    public class ObterClientePorIdQuery : IRequest<ClienteDTO>
    {
        public int ClienteId { get; set; }

        public ObterClientePorIdQuery(int clienteId)
        {
            ClienteId = clienteId;
        }
    }
}

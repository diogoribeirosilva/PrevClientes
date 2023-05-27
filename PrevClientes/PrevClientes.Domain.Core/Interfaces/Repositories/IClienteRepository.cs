using PrevClientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevClientes.Domain.Core.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<int> CriarCliente(Cliente cliente);
        Task<Cliente> ObterClientePorId(int id);
        Task<IEnumerable<Cliente>> ObterTodosClientes();
        Task AtualizarCliente(Cliente cliente);
        Task RemoverCliente(int id);
    }
}

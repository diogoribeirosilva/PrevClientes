using Microsoft.EntityFrameworkCore;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using PrevClientes.Domain.Models;
using PrevClientes.Infrastruture.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrevClientes.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SqlContext _dbContext;

        public ClienteRepository(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CriarCliente(Cliente cliente)
        {
            _dbContext.Cliente.Add(cliente);
            await _dbContext.SaveChangesAsync();
            return cliente.Id;
        }

        public async Task<Cliente> ObterClientePorId(int id)
        {
            return await _dbContext.Cliente.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosClientes()
        {
            return await _dbContext.Cliente.ToListAsync();
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            _dbContext.Entry(cliente).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoverCliente(int id)
        {
            var cliente = await _dbContext.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _dbContext.Cliente.Remove(cliente);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

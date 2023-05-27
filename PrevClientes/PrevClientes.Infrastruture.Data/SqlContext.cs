using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrevClientes.Domain.Models;

namespace PrevClientes.Infrastruture.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(){}

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlContext).Assembly);
        }
    }
}

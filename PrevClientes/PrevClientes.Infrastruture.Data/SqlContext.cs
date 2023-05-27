using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevClientes.Infrastruture.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {
        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }
    }
}

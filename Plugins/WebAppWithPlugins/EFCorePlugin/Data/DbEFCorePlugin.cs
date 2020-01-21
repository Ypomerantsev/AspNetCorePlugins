using EFCorePlugin.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCorePlugin.Data
{
    public class DbEFCorePlugin : DbContext
    {

        public DbEFCorePlugin(DbContextOptions<DbEFCorePlugin> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

    }
}

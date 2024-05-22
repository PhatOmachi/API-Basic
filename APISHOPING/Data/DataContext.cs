using APISHOPING.Enitty;
using Microsoft.EntityFrameworkCore;

namespace APISHOPING.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

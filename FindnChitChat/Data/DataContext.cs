using FindnChitChat.Model;
using Microsoft.EntityFrameworkCore;

namespace FindnChitChat.Data
{
    public class DataContext : DbContext
    {
         public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Value> Values { get; set; }
    }
}
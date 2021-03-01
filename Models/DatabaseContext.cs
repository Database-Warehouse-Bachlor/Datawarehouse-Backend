
using Microsoft.EntityFrameworkCore;

namespace Datawarehouse_Backend.Models {
    public class DatabaseContext : DbContext {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            Database.EnsureCreated();
         }
        public DbSet<User> users { get; set; }
    }

}

using Datawarehouse_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Datawarehouse_Backend.Context {
    public class LoginDatabaseContext : DbContext {
        public LoginDatabaseContext(DbContextOptions<LoginDatabaseContext> options) : base(options) {
            
        }

         // Define databse tables
        public DbSet<User> users { get; set; }

           protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasOne(u => u.tennant) 
            .WithMany(a => a.users) 
            .HasForeignKey(u => u.tennant.id);
    }
    }
    }

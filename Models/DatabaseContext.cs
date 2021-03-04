
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Datawarehouse_Backend.Models;

namespace Datawarehouse_Backend.Models {
    public class DatabaseContext : IdentityDbContext<User, Role, Guid> {
         private readonly DbContextOptions _options;
         public DbSet<User> Users { get; set;}
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            _options = options;
         }
        public DbSet<User> users { get; set; }
    }

}

using System.Linq;
using Datawarehouse_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Datawarehouse_Backend.Context
{

    public class LoginDatabaseContext : DbContext, ILoginDatabaseContext
    {
        public LoginDatabaseContext(DbContextOptions<LoginDatabaseContext> options) : base(options)
        {

        }

        // Define databse tables
        public DbSet<User> Users { get; set; }

        /*
        * A function to find the correct User based on email.
        */
        public User findUserByMail(string email)
        {
            var user = Users
            .Where(e => e.Email == email)
            .FirstOrDefault<User>();
            return user;
        }
    }
}

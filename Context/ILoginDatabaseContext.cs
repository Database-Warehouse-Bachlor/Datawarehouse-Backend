using System.Diagnostics.CodeAnalysis;
using Datawarehouse_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Datawarehouse_Backend.Context
{
    public interface ILoginDatabaseContext
    {
        public DbSet<User> Users { get; set; }
        User findUserByMail(string email);
        void setAdded(object entity);
        int SaveChanges();
    }

    



}
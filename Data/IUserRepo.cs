using System.Collections.Generic;
using Datawarehouse_Backend.Models;

namespace Datawarehouse_Backend.Data
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
    }
}
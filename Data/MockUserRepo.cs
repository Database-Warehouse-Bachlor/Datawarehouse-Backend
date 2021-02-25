using System.Collections.Generic;
using Datawarehouse_Backend.Models;

namespace Datawarehouse_Backend.Data
{
    public class MockUserRepo : IUserRepo
    {
        public IEnumerable<User> GetAllUsers()
        {
            var users = new List<User>
            {
            new User{Id=0, Email="Sig@gmail.com", password="123"},
            new User{Id=1, Email="Sige@gmail.com", password="321"},
            new User{Id=2, Email="Sigen@gmail.com", password="1234"},
            };
            return users;
        }
        

        public User GetUserById(int id)
        {
            return new User{Id=0, Email="Sig@gmail.com", password="123"};
        }
    }
}
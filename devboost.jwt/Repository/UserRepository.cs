using devboost.jwt.Models;
using System.Collections.Generic;
using System.Linq;

namespace devboost.jwt.Repository
{
    public static class UserRepository
    {

        public static User Get(string username, string password)
        {

            List<User> users = new List<User>() 
            { 
                new User() { Id = 0, UserName = "Marco", Password = "123456", Role = "Admin" }, 
                new User() { Id = 1, UserName = "Eric", Password = "9517539", Role = "Teacher" }
            };

            return users.Where(_ => _.UserName == username && _.Password == password).FirstOrDefault();

        }

        public static List<User> GetAllProfessores()
        {

            List<User> users = new List<User>()
            {
                new User() { Id = 0, UserName = "Marco", Password = "123456", Role = "Admin" },
                new User() { Id = 1, UserName = "Eric", Password = "9517539", Role = "Teacher" },
                new User() { Id = 1, UserName = "Felipe", Password = "564654", Role = "Teacher" },
                new User() { Id = 1, UserName = "Jefferson", Password = "121245", Role = "Teacher" }
            };

            return users.Where(_ => _.Role == "Teacher").ToList();

        }

    }
}

﻿using ApiSmartHome.Data.Models;

namespace ApiSmartHome.Data.Repository
{
    public class UserRepository : IUserRepository
    {
      
        public async Task<User[]> GetUsers()
        {
            var result = new User[] { new User()
            {                
                Id = Guid.NewGuid(),
                FirstName = "fname1",
                LastName = "lname1",
                Email ="1@mail.ru",
                Password= "11",
                Role = new Role(RoleType.Administrator)
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "fname2",
                LastName = "lname2",
                Email ="2@mail.ru",
                Password= "11",
                Role = new Role(RoleType.User)
            }

            };

            return result;
        }
    }
}

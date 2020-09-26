using System;
using System.Collections.Generic;
using System.Linq;
using LoginExample.Models;

namespace LoginExample.Data.Impl {
public class InMemoryUserService : IUserService {
    private List<User> users;

    public InMemoryUserService() {
        users = new[] {
            new User {
                City = "Horsens",
                Domain = "via.dk",
                Password = "123456",
                Role = "Teacher",
                BirthYear = 1986,
                SecurityLevel = 5,
                UserName = "Troels"
            },
            new User {
                City = "Aarhus",
                Domain = "hotmail.com",
                Password = "123456",
                Role = "Student",
                BirthYear = 1998,
                SecurityLevel = 3,
                UserName = "Jakob"
            },
            new User {
                City = "Vejle",
                Domain = "via.com",
                Password = "123456",
                Role = "Guest",
                BirthYear = 1973,
                SecurityLevel = 1,
                UserName = "Kasper"
            }
        }.ToList();
    }


    public User ValidateUser(string userName, string password) {
        User first = users.FirstOrDefault(user => user.UserName.Equals(userName));
        if (first == null) {
            throw new Exception("User not found");
        }

        if (!first.Password.Equals(password)) {
            throw new Exception("Incorrect password");
        }

        return first;
    }
}
}
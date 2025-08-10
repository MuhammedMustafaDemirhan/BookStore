using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Users
    {
        public static void AddUsers(this BookStoreDbContext context)
        {
            context.Users.AddRange(
                new User
                {
                    Name = "Ali",
                    Surname = "Yılmaz",
                    Email = "ali.yilmaz@example.com",
                    Password = "123456",
                    RefreshToken = "token1",
                    RefreshTokenExpireDate = DateTime.Now.AddDays(7)
                },
                new User
                {
                    Name = "Zeynep",
                    Surname = "Demir",
                    Email = "zeynep.demir@example.com",
                    Password = "zeynep123",
                    RefreshToken = "token2",
                    RefreshTokenExpireDate = DateTime.Now.AddDays(5)
                },
                new User
                {
                    Name = "Mehmet",
                    Surname = "Kara",
                    Email = "mehmet.kara@example.com",
                    Password = "mehmetpass",
                    RefreshToken = "token3",
                    RefreshTokenExpireDate = DateTime.Now.AddDays(10)
                },
                new User
                {
                    Name = "Ayşe",
                    Surname = "Çelik",
                    Email = "ayse.celik@example.com",
                    Password = "ayse456",
                    RefreshToken = "token4",
                    RefreshTokenExpireDate = DateTime.Now.AddDays(2)
                }
            );
        }
    }
}
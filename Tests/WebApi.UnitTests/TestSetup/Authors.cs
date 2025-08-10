using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author
                    {
                        Name = "Eric",
                        Surname = "Ries",
                        DateOfBirth = new DateTime(1978, 9, 22)
                    },
                    new Author
                    {
                        Name = "Charlotte Perkins",
                        Surname = "Gilman",
                        DateOfBirth = new DateTime(1860, 7, 03)
                    },
                    new Author
                    {
                        Name = " Frank",
                        Surname = "Herbert",
                        DateOfBirth = new DateTime(1920, 10, 08)
                    }, new Author
                    {
                        Name = "Orhan",
                        Surname = "Pamuk",
                        DateOfBirth = new DateTime(1952, 6, 7)
                    },
                    new Author
                    {
                        Name = "Elif",
                        Surname = "Şafak",
                        DateOfBirth = new DateTime(1971, 10, 25)
                    },
                    new Author
                    {
                        Name = "Yaşar",
                        Surname = "Kemal",
                        DateOfBirth = new DateTime(1923, 10, 6)
                    },
                    new Author
                    {
                        Name = "Ahmet",
                        Surname = "Ümit",
                        DateOfBirth = new DateTime(1960, 9, 9)
                    },
                    new Author
                    {
                        Name = "Ayşe",
                        Surname = "Kulin",
                        DateOfBirth = new DateTime(1941, 8, 26)
                    }
                    );
        }
    }
}
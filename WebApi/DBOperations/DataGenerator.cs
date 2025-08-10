using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
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

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

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


                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreID = 1,
                        AuthorId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreID = 2,
                        AuthorId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreID = 2,
                        AuthorId = 3,
                        PageCount = 540,
                        PublishDate = new DateTime(2002, 12, 21)
                    }
                        );
                context.SaveChanges();
            }
        }
    }
}
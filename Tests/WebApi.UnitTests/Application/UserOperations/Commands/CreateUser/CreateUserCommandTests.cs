using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests : IClassFixture<CommonTestFixture>, IDisposable
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;

            // Test başlamadan önce temizle (garanti için)
            ClearUsers();
        }

        private void ClearUsers()
        {
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            // Test bittiğinde temizle
            ClearUsers();
        }

        [Fact]
        public void WhenAlreadyExistEmailIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            var user = new User
            {
                Name = "Existing",
                Surname = "User",
                Email = "existinguser@example.com",
                Password = "123456"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = new CreateUserCommand.CreateUserModel
            {
                Name = "NewName",
                Surname = "NewSurname",
                Email = user.Email, // Aynı email
                Password = "newpassword"
            };

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Kullanıcı zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            var uniqueEmail = $"testuser_{Guid.NewGuid()}@example.com"; // Benzersiz email
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            CreateUserCommand.CreateUserModel model = new CreateUserCommand.CreateUserModel
            {
                Name = "Test",
                Surname = "User",
                Email = uniqueEmail,
                Password = "testpassword"
            };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
            user.Should().NotBeNull();
            user.Name.Should().Be(model.Name);
            user.Surname.Should().Be(model.Surname);
        }
    }
}

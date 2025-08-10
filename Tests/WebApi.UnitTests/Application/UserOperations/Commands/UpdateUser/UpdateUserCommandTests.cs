using FluentAssertions;
using System;
using System.Linq;
using TestSetup;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateUserCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenUserIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            UpdateUserCommand command = new UpdateUserCommand(_context);
            command.UserId = -1; // Var olmayan Id
            command.Model = new UpdateUserModel
            {
                Name = "Test",
                Surname = "Test",
                Email = "test@example.com",
                Password = "123456"
            };

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Kullanıcı Bulunamadı!");
        }

        [Fact]
        public void WhenEmailAlreadyExists_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            var existingUser = new User { Name = "Ali", Surname = "Veli", Email = "ali@example.com", Password = "1234" };
            _context.Users.Add(existingUser);
            _context.SaveChanges();

            var userToUpdate = new User { Name = "Mehmet", Surname = "Kara", Email = "mehmet@example.com", Password = "1234" };
            _context.Users.Add(userToUpdate);
            _context.SaveChanges();

            UpdateUserCommand command = new UpdateUserCommand(_context);
            command.UserId = userToUpdate.Id;
            command.Model = new UpdateUserModel
            {
                Name = "Mehmet",
                Surname = "Kara",
                Email = existingUser.Email,  // Zaten var olan email
                Password = "5678"
            };

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Aynı Email Sistemde Zaten Kayıtlı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeUpdated()
        {
            // Arrange
            var user = new User { Name = "OldName", Surname = "OldSurname", Email = "oldemail@example.com", Password = "oldpass" };
            _context.Users.Add(user);
            _context.SaveChanges();

            UpdateUserCommand command = new UpdateUserCommand(_context);
            command.UserId = user.Id;
            command.Model = new UpdateUserModel
            {
                Name = "NewName",
                Surname = "NewSurname",
                Email = "newemail@example.com",
                Password = "newpass"
            };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var updatedUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);
            updatedUser.Should().NotBeNull();
            updatedUser.Name.Should().Be(command.Model.Name);
            updatedUser.Surname.Should().Be(command.Model.Surname);
            updatedUser.Email.Should().Be(command.Model.Email);
            updatedUser.Password.Should().Be(command.Model.Password);
        }
    }
}

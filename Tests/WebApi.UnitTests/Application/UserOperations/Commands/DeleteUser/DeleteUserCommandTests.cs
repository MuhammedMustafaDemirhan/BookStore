using FluentAssertions;
using System;
using System.Linq;
using TestSetup;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteUserCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenUserIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            DeleteUserCommand command = new DeleteUserCommand(_context);
            command.UserId = -1; // Geçersiz ID

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Kullanıcı Bulunamadı!");
        }

        [Fact]
        public void WhenValidUserIdIsGiven_User_ShouldBeDeleted()
        {
            // Arrange
            var user = new User { Name = "Test", Surname = "User", Email = "testuser@example.com", Password = "123456" };
            _context.Users.Add(user);
            _context.SaveChanges();

            DeleteUserCommand command = new DeleteUserCommand(_context);
            command.UserId = user.Id;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var deletedUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);
            deletedUser.Should().BeNull();
        }
    }
}

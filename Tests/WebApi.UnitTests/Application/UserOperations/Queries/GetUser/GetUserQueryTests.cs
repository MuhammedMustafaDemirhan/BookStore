using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using TestSetup;
using WebApi.Application.UserOperations.Queries.GetUsers;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Queries.GetUser
{
    public class GetUserQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public GetUserQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenUsersExist_Handle_ShouldReturnUserList()
        {
            // Arrange
            _context.Users.AddRange(
                new User { Name = "Test1", Surname = "User1", Email = "test1@example.com", Password = "pass1" },
                new User { Name = "Test2", Surname = "User2", Email = "test2@example.com", Password = "pass2" }
            );
            _context.SaveChanges();

            GetUsersQuery query = new GetUsersQuery(_context, _mapper);

            // Act
            List<UsersViewModel> result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterOrEqualTo(2);
            result.Any(u => u.Email == "test1@example.com").Should().BeTrue();
            result.Any(u => u.Email == "test2@example.com").Should().BeTrue();
        }

        [Fact]
        public void WhenNoUsersExist_Handle_ShouldReturnEmptyList()
        {
            // Arrange
            // Veritabanındaki kullanıcıları temizle
            foreach (var user in _context.Users.ToList())
                _context.Users.Remove(user);
            _context.SaveChanges();

            GetUsersQuery query = new GetUsersQuery(_context, _mapper);

            // Act
            List<UsersViewModel> result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}

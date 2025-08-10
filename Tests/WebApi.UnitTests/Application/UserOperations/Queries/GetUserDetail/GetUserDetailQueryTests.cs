using FluentAssertions;
using System;
using System.Linq;
using TestSetup;
using WebApi.Application.UserOperations.Queries.GetUserDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public GetUserDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenUserIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
            query.UserId = -1;

            // Act & Assert
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Kullanıcı Bulunamadı!");
        }

        [Fact]
        public void WhenUserIdExists_UserDetail_ShouldBeReturned()
        {
            // Arrange
            var user = new User { Name = "Test", Surname = "User", Email = "testuser@example.com", Password = "123456" };
            _context.Users.Add(user);
            _context.SaveChanges();

            GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
            query.UserId = user.Id;

            // Act
            UserDetailViewModel result = FluentActions.Invoking(() => query.Handle()).Invoke();

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
            result.Surname.Should().Be(user.Surname);
            result.Email.Should().Be(user.Email);
            result.Password.Should().Be(user.Password);
        }
    }
}

using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenCalled_Handle_ShouldReturnAuthorsList()
        {
            // Arrange
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
            result.All(a => !string.IsNullOrEmpty(a.Name)).Should().BeTrue();
            result.All(a => !string.IsNullOrEmpty(a.Surname)).Should().BeTrue();
        }
    }
}

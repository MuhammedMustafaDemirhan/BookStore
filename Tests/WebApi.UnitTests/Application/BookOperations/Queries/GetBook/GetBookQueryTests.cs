using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Queries.GetBook
{
    public class GetBookQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenBooksExistInDatabase_GetBooksQuery_ShouldReturnBookList()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }
    }
}
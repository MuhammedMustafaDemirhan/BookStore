using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Handle_ShouldReturnBookDetail()
        {
            var book = new Book()
            {
                Title = "Lord O.",
                GenreID = 1,
                AuthorId = 1,
                PageCount = 150,
                PublishDate = new DateTime(1999, 01, 01)
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            var id = _context.Books.Where(b => b.Title == book.Title).Select(b => b.ID).FirstOrDefault();

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;

            var result = query.Handle();

            result.Should().NotBeNull();
            result.Title.Should().Be(book.Title);
            result.PageCount.Should().Be(book.PageCount);
            result.Genre.Should().NotBeNullOrEmpty();
            result.Author.Should().NotBeNullOrEmpty();
            result.PublishDate.Should().NotBeNullOrEmpty();
        }
    }
}
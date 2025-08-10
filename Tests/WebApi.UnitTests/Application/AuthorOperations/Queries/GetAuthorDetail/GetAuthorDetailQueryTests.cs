using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Handle_ShouldReturnAuthorDetail()
        {
            // Arrange
            var author = new Author { Name = "Test", Surname = "Author", DateOfBirth = new DateTime(1970, 1, 1) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var id = author.Id;

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(author.Name);
            result.Surname.Should().Be(author.Surname);
            result.DateOfBirth.Should().Be(author.DateOfBirth);
        }

        [Fact]
        public void WhenInvalidAuthorIdIsGiven_Handle_ShouldThrowInvalidOperationException()
        {
            // Arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = -1;

            // Act & Assert
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Yazar Bulunamadı!");
        }
    }
}

using AutoMapper;
using FluentAssertions;
using System;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using TestSetup;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_Handle_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = -1; // geçersiz ID

            // Act & Assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Handle_ShouldReturnGenreDetail()
        {
            // Arrange
            var genre = new Genre { Name = "TestGenre", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = genre.Id;

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(genre.Name);
            result.Id.Should().Be(genre.Id);
        }
    }
}

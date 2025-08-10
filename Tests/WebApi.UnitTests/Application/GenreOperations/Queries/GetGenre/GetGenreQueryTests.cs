using AutoMapper;
using FluentAssertions;
using System.Linq;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using TestSetup;

namespace Application.GenreOperations.Queries.GetGenre
{
    public class GetGenreQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenGenresExist_Handle_ShouldReturnGenreList()
        {
            // Arrange - test verisi ekle
            var genre1 = new Genre { Name = "TestGenre1", IsActive = true };
            var genre2 = new Genre { Name = "TestGenre2", IsActive = true };
            var genre3 = new Genre { Name = "InactiveGenre", IsActive = false };

            _context.Genres.AddRange(genre1, genre2, genre3);
            _context.SaveChanges();

            var query = new GetGenresQuery(_context, _mapper);

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterOrEqualTo(2);
            result.Any(x => x.Name == "TestGenre1").Should().BeTrue();
            result.Any(x => x.Name == "TestGenre2").Should().BeTrue();
            result.Any(x => x.Name == "InactiveGenre").Should().BeFalse();
        }
    }
}

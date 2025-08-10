using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Handle_ShouldDeleteGenre()
        {
            var genre = new Genre() { Name = "Drama" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var id = _context.Genres.Where(g => g.Name == genre.Name).Select(genre => genre.Id).FirstOrDefault();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            command.Handle();

            var deletedGenre = _context.Genres.SingleOrDefault(g => g.Id == id);
            deletedGenre.Should().BeNull();
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_Handle_ShouldThrowInvalidOperationException()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = -1;

            FluentActions
                    .Invoking(() => command.Handle())
                    .Should()
                    .Throw<InvalidOperationException>()
                    .WithMessage("Kitap Türü Bulunamadı!");
        }
    }
}

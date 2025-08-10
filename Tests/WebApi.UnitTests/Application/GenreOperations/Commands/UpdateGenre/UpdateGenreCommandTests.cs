using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            // Arrange: var olan bir tür ekle
            var genre = new Genre() { Name = "Science" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var command = new UpdateGenreCommand(_context);
            command.GenreId = genre.Id;
            command.Model = new UpdateGenreModel()
            {
                Name = "Science Updated",
                IsActive = false
            };

            // Act
            Action act = () => command.Handle();

            // Assert
            act.Should().NotThrow();

            var updatedGenre = _context.Genres.SingleOrDefault(x => x.Id == genre.Id);
            updatedGenre.Name.Should().Be("Science Updated");
            updatedGenre.IsActive.Should().BeFalse();
        }

        [Fact]
        public void WhenGenreIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new UpdateGenreCommand(_context);
            command.GenreId = -1; // var olmayan Id
            command.Model = new UpdateGenreModel() { Name = "AnyName", IsActive = true };

            Action act = () => command.Handle();

            act.Should().Throw<InvalidOperationException>().WithMessage("Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenGenreNameAlreadyExists_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange: iki farklı tür ekle
            var genre1 = new Genre() { Name = "History" };
            var genre2 = new Genre() { Name = "Math" };
            _context.Genres.AddRange(genre1, genre2);
            _context.SaveChanges();

            var command = new UpdateGenreCommand(_context);
            command.GenreId = genre2.Id;
            command.Model = new UpdateGenreModel() { Name = "History", IsActive = true };

            // Act
            Action act = () => command.Handle();

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Aynı isimli bir kitap türü zaten mevcut");
        }
    }
}

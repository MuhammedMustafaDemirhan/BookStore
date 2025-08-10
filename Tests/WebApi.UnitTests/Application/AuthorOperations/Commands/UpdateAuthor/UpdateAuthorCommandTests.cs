using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenAuthorNotFound_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            var command = new UpdateAuthorCommand(_context)
            {
                AuthorId = -1, // olmayan bir ID
                Model = new UpdateAuthorCommand.UpdateAuthorModel
                {
                    Name = "UpdatedName",
                    Surname = "UpdatedSurname",
                    DateOfBirth = new DateTime(1990, 1, 1)
                }
            };

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            // Arrange
            var author = new Author { Name = "Test", Surname = "Author", DateOfBirth = new DateTime(1980, 1, 1) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var command = new UpdateAuthorCommand(_context)
            {
                AuthorId = author.Id,
                Model = new UpdateAuthorCommand.UpdateAuthorModel
                {
                    Name = "UpdatedName",
                    Surname = "UpdatedSurname",
                    DateOfBirth = new DateTime(1975, 5, 5)
                }
            };

            // Act
            command.Handle();

            // Assert
            var updatedAuthor = _context.Authors.Single(x => x.Id == author.Id);
            updatedAuthor.Name.Should().Be("UpdatedName");
            updatedAuthor.Surname.Should().Be("UpdatedSurname");
            updatedAuthor.DateOfBirth.Should().Be(new DateTime(1975, 5, 5));
        }
    }
}

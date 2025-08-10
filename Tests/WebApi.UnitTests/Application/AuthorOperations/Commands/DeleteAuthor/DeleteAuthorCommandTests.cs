using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public void WhenAuthorHasBook_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            var author = new Author { Name = "HasBook", Surname = "Author", DateOfBirth = new DateTime(1980, 1, 1) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var book = new Book { Title = "TestBook", PageCount = 100, PublishDate = new DateTime(2001, 01, 01), GenreID = 1, AuthorId = author.Id };
            _context.Books.Add(book);
            _context.SaveChanges();

            var command = new DeleteAuthorCommand(_context) { AuthorId = author.Id };

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().WithMessage("Yayında Kitabı Olan Bir Yazar Silinemez!");
        }

        [Fact]
        public void WhenAuthorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            var command = new DeleteAuthorCommand(_context) { AuthorId = 9999 };

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().WithMessage("Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeDeleted()
        {
            // Arrange
            var author = new Author { Name = "Deletable", Surname = "Author", DateOfBirth = new DateTime(1975, 1, 1) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var command = new DeleteAuthorCommand(_context) { AuthorId = author.Id };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var deletedAuthor = _context.Authors.SingleOrDefault(x => x.Id == author.Id);
            deletedAuthor.Should().BeNull();
        }
    }
}

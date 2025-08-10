using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenBookIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 999; // olmayan kitap
            command.Model = new UpdateBookCommand.UpdateBookModel()
            {
                Title = "New Title",
                GenreID = 1,
                AuthorId = 1
            };

            // Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Güncellenecek Kitap bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // Arrange - önce bir kitap ekleyelim
            var book = new Book()
            {
                Title = "Old Title",
                PageCount = 200,
                PublishDate = new DateTime(1999, 01, 01),
                GenreID = 2,
                AuthorId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            var bookToUpdate = _context.Books.FirstOrDefault(b => b.Title == "Old Title");

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookToUpdate.ID;
            command.Model = new UpdateBookCommand.UpdateBookModel()
            {
                Title = "Updated Title",
                GenreID = 1,
                AuthorId = 1 // Bu kullanılmıyor ama modelde var
            };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var updatedBook = _context.Books.FirstOrDefault(b => b.ID == bookToUpdate.ID);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be("Updated Title");
            updatedBook.GenreID.Should().Be(1);
        }
    }
}

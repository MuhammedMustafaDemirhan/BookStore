using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenBookIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 999;

            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Silinecek kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleed()
        {
            var book = new Book()
            {
                Title = "DeleteTestBook",
                PageCount = 123,
                PublishDate = new DateTime(2001, 01, 01),
                GenreID = 1,
                AuthorId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            var createdBook = _context.Books.FirstOrDefault(b => b.Title == "DeleteTestBook");

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = createdBook.ID;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var deletedBook = _context.Books.SingleOrDefault(b => b.ID == command.BookId);
            deletedBook.Should().BeNull();
        }
    }
}
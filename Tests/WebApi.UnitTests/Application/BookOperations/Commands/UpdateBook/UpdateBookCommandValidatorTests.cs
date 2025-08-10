using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "", 0, 0)] // hepsi hatalı
        [InlineData(1, "", 0, 0)] // boş title, genre ve author hatalı
        [InlineData(1, "abc", 1, 1)] // title < 4 karakter
        [InlineData(1, "ValidTitle", 0, 1)] // genreId geçersiz
        [InlineData(1, "ValidTitle", 1, 0)] // authorId geçersiz
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(int bookId, string title, int genreId, int authorId)
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookCommand.UpdateBookModel()
            {
                Title = title,
                GenreID = genreId,
                AuthorId = authorId
            };

            // Act
            UpdateBookCommandValidation validator = new UpdateBookCommandValidation();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookCommand.UpdateBookModel()
            {
                Title = "Updated Title",
                GenreID = 1,
                AuthorId = 1
            };

            // Act
            UpdateBookCommandValidation validator = new UpdateBookCommandValidation();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

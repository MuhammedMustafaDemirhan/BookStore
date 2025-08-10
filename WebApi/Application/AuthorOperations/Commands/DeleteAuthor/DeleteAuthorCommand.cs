using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var hasBooks = _context.Books.Any(x => x.AuthorId == AuthorId);
            if (hasBooks)
                throw new InvalidOperationException("Yayında Kitabı Olan Bir Yazar Silinemez!");


            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar Bulunamadı!");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
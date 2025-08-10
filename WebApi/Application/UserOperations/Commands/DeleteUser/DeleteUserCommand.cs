using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public int UserId;
        public IBookStoreDbContext _context;
        public DeleteUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == UserId);
            if (user is null)
                throw new InvalidOperationException("Kullanıcı Bulunamadı!");
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
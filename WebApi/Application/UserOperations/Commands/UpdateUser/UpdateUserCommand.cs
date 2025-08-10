using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        public int UserId { get; set; }
        public UpdateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public UpdateUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == UserId);
            if (user is null)
                throw new InvalidOperationException("Kullanıcı Bulunamadı!");
            if (_context.Users.Any(u => u.Email.ToLower() == Model.Email.ToLower() && u.Id != UserId))
                throw new InvalidOperationException("Aynı Email Sistemde Zaten Kayıtlı!");
            user.Name = !string.IsNullOrWhiteSpace(Model.Name) ? Model.Name : user.Name;
            user.Surname = !string.IsNullOrWhiteSpace(Model.Surname) ? Model.Surname : user.Surname;
            user.Email = !string.IsNullOrWhiteSpace(Model.Email) ? Model.Email : user.Email;
            user.Password = !string.IsNullOrWhiteSpace(Model.Password) ? Model.Password : user.Password;

            _context.SaveChanges();
        }
    }
    public class UpdateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
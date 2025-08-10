using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Queries.GetUsers
{
    public class GetUsersQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<UsersViewModel> Handle()
        {
            var users = _context.Users.ToList<User>();
            List<UsersViewModel> vm = _mapper.Map<List<UsersViewModel>>(users);
            return vm;
        }
    }

    public class UsersViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
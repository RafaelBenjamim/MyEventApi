using MyEventApi.Core.Entities;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MyEventApi.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context  )
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByEmail (string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        } 

        public async Task AddUser(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}

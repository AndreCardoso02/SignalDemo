using Microsoft.EntityFrameworkCore;
using SignalRWithEntityFramework.Models;

namespace SignalRWithEntityFramework.Repository
{
    public class UserRepository
    {
        private readonly SignalRnotificationDbContext _context;

        public UserRepository(SignalRnotificationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserDetails(string username, string password)
        {
           return await _context.Users.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }
}

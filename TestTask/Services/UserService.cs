using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Data;
using Microsoft.EntityFrameworkCore;
using TestTask.Enums;

namespace TestTask.Services
{
    public class UserService : IUserService
    {
        ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser()
        {
            var users= _context.Users.
                GroupJoin(_context.Orders,
                    user => user,
                    order => order.User,
                    (user, orders) =>
                        new { Id = user.Id, OrdersNum = orders.Count() })
                .ToList();

            var MaxCount = users.Max(x => x.OrdersNum);
            var Id = users.First(x => x.OrdersNum == MaxCount).Id;

            return await _context.Users.FirstOrDefaultAsync(user => user.Id == Id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users
                .Where(user => user.Status == UserStatus.Inactive)
                .ToListAsync();
        }
    }
}

using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Data;
using System.Reflection.Metadata.Ecma335;

namespace TestTask.Services
{
    public class OrderService : IOrderService
    {
        ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<Order> GetOrder()
        {
            int MaxPrice = _context.Orders.Max(x => x.Price * x.Quantity);
            return  _context.Orders.First(x => x.Price * x.Quantity == MaxPrice);
        }

        public async Task<List<Order>> GetOrders()
        {
            return _context.Orders.Where(x => x.Quantity > 10).ToList();
        }
    }
}

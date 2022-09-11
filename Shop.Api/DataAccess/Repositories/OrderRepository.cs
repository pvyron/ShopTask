using Microsoft.EntityFrameworkCore;
using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Models.DbModels;

namespace Shop.Api.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<ICollection<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public virtual async Task<Order?> GetByIdAsync(Guid id)
        {
            Order? order = await _dbContext.Orders.FindAsync(id);

            return order;
        }

        public virtual async Task<Order> CreateAsync(Order order)
        {
            Order newOrder = (await _dbContext.Orders.AddAsync(order)).Entity;

            return newOrder;
        }

        public virtual async Task<Order?> UpdateAsync(Order order)
        {
            return _dbContext.Orders.Update(order).Entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            Order? order = await GetByIdAsync(id);

            if (order is null)
                throw new NullReferenceException();

            _dbContext.Orders.Remove(order);
        }

        public virtual async Task<ICollection<Order>> GetOrdersOfCustomer(Guid customerId)
        {
            return await _dbContext.Orders.Where(o => o.CustomerId == customerId).OrderBy(o => o.DocDate).ToListAsync();
        }
    }
}

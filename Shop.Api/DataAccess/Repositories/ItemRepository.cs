using Microsoft.EntityFrameworkCore;
using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Models.DbModels;

namespace Shop.Api.DataAccess.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<ICollection<Item>> GetAllAsync()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public virtual async Task<Item?> GetByIdAsync(Guid id)
        {
            Item? item = await _dbContext.Items.FindAsync(id);

            return item;
        }

        public virtual async Task<Item> CreateAsync(Item item)
        {
            Item newItem = (await _dbContext.Items.AddAsync(item)).Entity;

            return newItem;
        }

        public virtual async Task<Item?> UpdateAsync(Item item)
        {
            return _dbContext.Items.Update(item).Entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            Item? item = await GetByIdAsync(id);

            if (item is null)
                throw new NullReferenceException();

            _dbContext.Items.Remove(item);
        }

        public virtual async Task<ICollection<Item>> GetItemsOfOrder(Guid orderId)
        {
            List<Item> items = await _dbContext.Items.Where(x => x.OrderId == orderId).ToListAsync();

            return items;
        }
    }
}

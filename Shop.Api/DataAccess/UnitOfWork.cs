using Shop.Api.DataAccess.Interfaces;
using Shop.Api.DataAccess.Repositories;

namespace Shop.Api.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public ICustomerRepository Customers { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductRepository Products { get; private set; }
        public IItemRepository Items { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Customers = new CustomerRepository(dbContext);
            Orders = new OrderRepository(dbContext);
            Products = new ProductRepository(dbContext);
            Items = new ItemRepository(dbContext);
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransaction()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                await _dbContext.Database.RollbackTransactionAsync();
        }
    }
}

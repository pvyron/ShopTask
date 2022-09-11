namespace Shop.Api.DataAccess.Interfaces;

public interface IUnitOfWork
{
    ICustomerRepository Customers { get; }
    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
    IItemRepository Items { get; }

    Task CompleteAsync();
    Task BeginTransaction();
    Task CommitTransaction();
    Task RollbackTransaction();
}

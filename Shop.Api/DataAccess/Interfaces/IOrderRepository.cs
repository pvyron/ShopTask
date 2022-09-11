using Shop.Api.Models.DbModels;

namespace Shop.Api.DataAccess.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<ICollection<Order>> GetOrdersOfCustomer(Guid customerId);
    }
}

using Shop.Api.Models.DbModels;

namespace Shop.Api.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrder(Guid id);
        Task<Order?> CreateOrder(Order order);
        Task<Order?> UpdateOrder(Guid id, Order order);
        Task<bool> DeleteOrder(Guid id);
        Task<List<Order>> GetOrdersOfCustomer(Guid customerId);
    }
}

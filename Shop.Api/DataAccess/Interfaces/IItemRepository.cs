using Shop.Api.Models.DbModels;

namespace Shop.Api.DataAccess.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<ICollection<Item>> GetItemsOfOrder(Guid orderId);
    }
}

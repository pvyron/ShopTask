using Shop.Api.Models.DbModels;

namespace Shop.Api.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProduct(Guid id);
        Task<Product?> CreateProduct(Product product);
        Task<Product?> UpdateProduct(Product product);
    }
}

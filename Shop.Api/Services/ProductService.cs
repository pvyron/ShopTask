using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Models.DbModels;

namespace Shop.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return (await _unitOfWork.Products.GetAllAsync()).ToList();
            }
            catch
            {
                return new List<Product>();
            }
        }

        public async Task<Product?> GetProduct(Guid id)
        {
            try
            {
                return await _unitOfWork.Products.GetByIdAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Product?> CreateProduct(Product Product)
        {
            try
            {
                Product newProduct = await _unitOfWork.Products.CreateAsync(Product);

                await _unitOfWork.CompleteAsync();

                return newProduct;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Product?> UpdateProduct(Product Product)
        {
            try
            {
                Product? updatedProduct = await _unitOfWork.Products.UpdateAsync(Product);

                await _unitOfWork.CompleteAsync();

                return updatedProduct;
            }
            catch
            {
                return null;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Models.DbModels;

namespace Shop.Api.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<ICollection<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public virtual async Task<Product?> GetByIdAsync(Guid id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);

            return product;
        }

        public virtual async Task<Product> CreateAsync(Product product)
        {
            Product newProduct = (await _dbContext.Products.AddAsync(product)).Entity;

            return newProduct;
        }

        public virtual async Task<Product?> UpdateAsync(Product product)
        {
            return _dbContext.Products.Update(product).Entity;
        }

        /// <summary>
        /// Can't be used
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Models.DbModels;

namespace Shop.Api.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<ICollection<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public virtual async Task<Customer?> GetByIdAsync(Guid id)
        {
            Customer? customer = await _dbContext.Customers.FindAsync(id);

            return customer;
        }

        public virtual async Task<Customer> CreateAsync(Customer customer)
        {
            Customer newCustomer = (await _dbContext.Customers.AddAsync(customer)).Entity;

            return newCustomer;
        }

        public virtual async Task<Customer?> UpdateAsync(Customer customer)
        {
            return _dbContext.Customers.Update(customer).Entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            Customer? customer = await GetByIdAsync(id);

            if (customer is null)
                throw new NullReferenceException();

            _dbContext.Customers.Remove(customer);
        }
    }
}

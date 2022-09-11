using Shop.Api.Models.DbModels;

namespace Shop.Api.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomer(Guid id);
        Task<Customer?> CreateCustomer(Customer customer);
        Task<Customer?> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(Guid id);
    }
}

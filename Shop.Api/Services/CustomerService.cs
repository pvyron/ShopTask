using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Models.DbModels;

namespace Shop.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                return (await _unitOfWork.Customers.GetAllAsync()).ToList();
            }
            catch
            {
                return new List<Customer>();
            }
        }

        public async Task<Customer?> GetCustomer(Guid id)
        {
            try
            {
                return await _unitOfWork.Customers.GetByIdAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Customer?> CreateCustomer(Customer customer)
        {
            try
            {
                Customer newCustomer = await _unitOfWork.Customers.CreateAsync(customer);

                await _unitOfWork.CompleteAsync();

                return newCustomer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Customer?> UpdateCustomer(Customer customer)
        {
            try
            {
                Customer? updatedCustomer = await _unitOfWork.Customers.UpdateAsync(customer);

                await _unitOfWork.CompleteAsync();

                return updatedCustomer;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            try
            {
                if ((await _unitOfWork.Orders.GetOrdersOfCustomer(id)).Count > 0)
                    throw new Exception("This customer has orders and cannot be deleted");

                await _unitOfWork.Customers.DeleteAsync(id);

                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

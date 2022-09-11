using Moq;
using NUnit.Framework;
using Shop.Api.DataAccess.Interfaces;
using Shop.Api.DataAccess.Repositories;
using Shop.Api.Models.DbModels;
using Shop.Api.Services;

namespace Shop.Api.Tests
{
    internal class CustomerServicesTest
    {
        private ICustomerService _customerService;
        private IUnitOfWork _unitOfWork;
        private List<Customer> _customers;
        private CustomerRepository _customerRepository;

        [SetUp]
        public void Setup()
        {
            _customers = SetUpCustomers();
            _customerRepository = SetUpCustomerRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.Customers).Returns(_customerRepository);
            _unitOfWork = unitOfWork.Object;
            _customerService = new CustomerService(_unitOfWork);
        }

        [TearDown]
        public void DisposeTest()
        {
            _customers = null;
            _customerService = null;
            _unitOfWork = null;
            _customerRepository = null;
        }

        [Test]
        public void GetAllCustomersTest()
        {
            var customers = _customerService.GetAllCustomers().Result;

            var comparer = new CustomerComparer();

            CollectionAssert.AreEqual(
                    customers.OrderBy(customer => customer, comparer),
                    _customers.OrderBy(customer => customer, comparer), comparer);
        }

        List<Customer> SetUpCustomers()
        {
            List<Customer> customers = DataInitializer.GetAllCustomers();

            foreach (var customer in customers)
            {
                customer.Id = new Guid();
            }

            return customers;
        }

        CustomerRepository SetUpCustomerRepository()
        {
            var mockRepo = new Mock<CustomerRepository>(MockBehavior.Default, null);

            mockRepo.Setup(p => p.GetAllAsync()).ReturnsAsync(_customers);

            mockRepo.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Func<Guid, Customer?>(id => _customers.Find(c => c.Id.Equals(id))));

            mockRepo.Setup(p => p.CreateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(new Func<Customer, Customer?>(newCustomer =>
                {
                    newCustomer.Id = new Guid();

                    _customers.Add(newCustomer);

                    return newCustomer;
                }));

            mockRepo.Setup(p => p.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(new Func<Customer, Customer?>(cust =>
                {
                    var oldCustomer = _customers.Find(exCust => exCust.Id == cust.Id);

                    if (oldCustomer is null)
                        return null;

                    oldCustomer = cust;

                    return oldCustomer;
                }));

            mockRepo.Setup(p => p.DeleteAsync(It.IsAny<Guid>())).Callback(new Action<Guid>(id =>
            {
                var customerToRemove = _customers.Find(exCust => exCust.Id == id);

                if (customerToRemove is not null)
                    _customers.Remove(customerToRemove);
            }));

            return mockRepo.Object;
        }
    }
}

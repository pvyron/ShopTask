using Castle.Core.Resource;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Shop.Api.Controllers;
using Shop.Api.DataAccess.Interfaces;
using Shop.Api.DataAccess.Repositories;
using Shop.Api.Handlers.Customers;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.RequestModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Services;
using System.Net;

namespace Shop.Api.Tests
{
    internal class CustomerApiTest
    {
        private ICustomerService _customerService;
        private IUnitOfWork _unitOfWork;
        private List<Customer> _customers;
        private CustomerRepository _customerRepository;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private GetAllCustomersHandler _getAllCustomersHandler;
        private GetCustomerByIdHandler _getCustomerByIdHandler;
        private CreateCustomerHandler _createCustomerHandler;
        private const string ServiceBaseURL = "http://localhost:7035/";

        [SetUp]
        public void Setup()
        {
            _customers = SetUpCustomers();
            _customerRepository = SetUpCustomerRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.Customers).Returns(_customerRepository);
            _unitOfWork = unitOfWork.Object;
            _customerService = new CustomerService(_unitOfWork);
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
        }

        [TearDown]
        public void DisposeAllObjects()
        {
            _customerService = null;
            _unitOfWork = null;
            _customerRepository = null;
            _customers = null;
            if (_response != null)
                _response.Dispose();
            if (_client != null)
                _client.Dispose();
        }

        [Test]
        public void GetAllCustomersTest()
        {
            _getAllCustomersHandler = new GetAllCustomersHandler(_customerService);
            List<CustomerResponseModel> customerResponseModels = _getAllCustomersHandler.Handle(new Queries.Customers.GetAllCustomersQuery(), new CancellationToken()).Result.ToList();

            var comparer = new CustomerApiComparer();
            CollectionAssert.AreEqual(
                    _customers.OrderBy(customer => customer.Id),
                    customerResponseModels.OrderBy(customer => customer.Id), comparer);
        }

        [Test]
        public void GetCustomerByWrongIdTest()
        {
            Guid wrongId = new Guid();

            _getCustomerByIdHandler = new GetCustomerByIdHandler(_customerService);
            CustomerResponseModel? customerResponseModels = _getCustomerByIdHandler.Handle(new Queries.Customers.GetCustomerByIdQuery(wrongId), new CancellationToken()).Result;

            if (_customers.Any(c => c.Id == wrongId)) //No chance
            {
                Assert.IsNotNull(customerResponseModels);
            }
            else
            {
                Assert.IsNull(customerResponseModels);
            }
        }

        [Test]
        public void CreateCustomerTest()
        {
            CustomerRequestModel customerRequestModel = new CustomerRequestModel()
            {
                FirstName = RandomString(10),
                LastName = RandomString(20),
                Address = RandomString(15),
                PostalCode = RandomString(5)
            };

            _createCustomerHandler = new CreateCustomerHandler(_customerService);
            CustomerResponseModel? customerResponseModel = _createCustomerHandler.Handle(new Commands.Customers.CreateCustomerCommand(customerRequestModel), new CancellationToken()).Result;

            var comparer = new CustomerApiComparer();
            Assert.Zero(comparer.Compare(customerRequestModel, customerResponseModel));
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

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

using Shop.Api.Models.DbModels;

namespace Shop.Api.Tests
{
    /// <summary>  
    /// Data initializer for unit tests  
    /// </summary>  
    public class DataInitializer
    {
        public static List<Product> GetAllProducts()
        {
            var products = new List<Product> {
                new Product()
                {
                    Name = "Glasses",
                    Price = 10
                },
                new Product()
                {
                    Name = "Shoes",
                    Price = 20
                },
                new Product()
                {
                    Name = "TV",
                    Price = 30
                }
            };

            return products;
        }

        public static List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer()
                {
                    FirstName = "Thea",
                    LastName = "Anderson",
                    Address = "18 North Pennsylvania Lane Vicksburg, MS",
                    PostalCode = "39180"
                },
                new Customer()
                {
                    FirstName = "Gabrielle",
                    LastName = "Klein",
                    Address = "268 West Galvin Ave. Westfield, MA",
                    PostalCode = "01085"
                },
                new Customer()
                {
                    FirstName = "Charlie",
                    LastName = "Payne",
                    Address = "5 South Saxton Street Reynoldsburg, OH",
                    PostalCode = "43068"
                },
                new Customer()
                {
                    FirstName = "Aimee",
                    LastName = "Bourne",
                    Address = "94 Old Circle St. Klamath Falls, OR",
                    PostalCode = "97603"
                },
                new Customer()
                {
                    FirstName = "Jacqueline",
                    LastName = "Stanton",
                    Address = "33 Lower River St. Marshfield, WI",
                    PostalCode = "54449"
                }
            };

            return customers;
        }
    }
}

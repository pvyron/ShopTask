namespace Shop.Api.Models.ResponseModels
{
    public class CustomerResponseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
    }
}

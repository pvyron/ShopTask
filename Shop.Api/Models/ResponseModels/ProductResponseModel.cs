namespace Shop.Api.Models.ResponseModels
{
    public class ProductResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}

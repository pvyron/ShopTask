namespace Shop.Api.Models.ResponseModels
{
    public class ItemResponseModel
    {
        public Guid Id { get; set; }
        public ProductResponseModel Product { get; set; } = null!;
        public decimal Quantity { get; set; }
    }
}

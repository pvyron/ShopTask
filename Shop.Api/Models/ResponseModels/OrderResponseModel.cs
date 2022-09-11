namespace Shop.Api.Models.ResponseModels
{
    public class OrderResponseModel
    {
        public Guid Id { get; set; }
        public CustomerResponseModel Customer { get; set; } = null!;
        public DateTime DocDate { get; set; }
        public decimal DocTotal { get; set; }
        public List<ItemResponseModel> Items { get; set; } = null!;
    }
}

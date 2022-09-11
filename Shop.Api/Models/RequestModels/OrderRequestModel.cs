using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.RequestModels
{
    public class OrderRequestModel
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public ICollection<ItemRequestModel> Items { get; set; } = null!;
    }
}

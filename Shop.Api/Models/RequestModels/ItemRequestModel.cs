using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.RequestModels
{
    public class ItemRequestModel
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal Quantity { get; set; }
    }
}

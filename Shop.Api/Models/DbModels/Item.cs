using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Api.Models.DbModels
{
    [Table("Items")]
    public class Item
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        [Required]
        public Product Product { get; set; } = null!;

        [Required]
        public decimal Quantity { get; set; }
    }
}

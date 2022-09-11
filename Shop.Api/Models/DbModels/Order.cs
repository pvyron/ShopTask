using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Api.Models.DbModels
{
    [Table("Orders")]
    [Index(nameof(DocDate))]
    public class Order
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }

        [Required]
        public Customer Customer { get; set; } = null!;

        [Required]
        public DateTime DocDate { get; set; }

        public decimal DocTotal { get; set; }

        [Required]
        public ICollection<Item> Items { get; set; } = null!;
    }
}

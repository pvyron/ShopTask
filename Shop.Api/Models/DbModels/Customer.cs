using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Api.Models.DbModels
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Full name is required")]
        public string FirstName { get; set; } = null!;

        [MaxLength(250)]
        [Required(ErrorMessage = "Full name is required")]
        public string LastName { get; set; } = null!;

        [MaxLength(50)]
        public string? Address { get; set; }

        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }
    }
}

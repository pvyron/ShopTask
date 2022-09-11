using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.RequestModels
{
    public class CustomerRequestModel
    {
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

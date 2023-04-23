using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Domain.Models.DTO
{
    public class OrderItem_Dto
    {
        [Required]
        public int BeerId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}

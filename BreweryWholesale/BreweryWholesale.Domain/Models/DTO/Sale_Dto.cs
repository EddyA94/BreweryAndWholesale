using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Domain.Models.DTO
{
    public class Sale_Dto
    {
        [Required]
        public int WholesalerId { get; set; }
        [Required]
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
}

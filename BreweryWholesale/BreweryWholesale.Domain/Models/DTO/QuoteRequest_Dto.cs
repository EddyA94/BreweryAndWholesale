using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Domain.Models.DTO
{
    public class QuoteRequest_Dto
    {
        [Required]
        public int WholesalerId { get; set; }
        [Required]
        public List<OrderItem_Dto>? OrderItems { get; set; }
    }
}

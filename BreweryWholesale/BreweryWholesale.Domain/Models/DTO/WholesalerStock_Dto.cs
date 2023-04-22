using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Domain.Models.DTO
{
    public class WholesalerStock_Dto
    {
        [Required]
        public int WholesalerStockId { get; set; }
        [Required]
        public int StockQuantity { get; set; }
    }
}

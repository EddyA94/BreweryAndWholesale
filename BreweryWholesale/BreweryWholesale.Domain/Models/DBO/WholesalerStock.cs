using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryWholesale.Domain.Models.DBO
{
    [Table(nameof(WholesalerStock))]
    [PrimaryKey(nameof(WholesalerStockID))]
    public class WholesalerStock
    {
        public int WholesalerStockID { get; set; }
        public int WholesalerID { get; set; }
        public int BeerID { get; set; }
        public int StockQuantity { get; set; }

        public Wholesaler Wholesaler { get; set; }
        public Beer Beer { get; set; }
        
    }
}

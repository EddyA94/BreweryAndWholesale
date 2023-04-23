
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryWholesale.Domain.Models.DBO
{
    [Table(nameof(Sale))]
    [PrimaryKey(nameof(SaleID))]
    public class Sale
    {
        public int SaleID { get; set; }
        public int WholesalerID { get; set; }
        public int BeerID { get; set; }
        public int Quantity { get; set; }
        public virtual Wholesaler? Wholesaler { get; set;}
        public virtual Beer? Beer { get; set; }
    }
}

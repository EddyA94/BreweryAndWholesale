using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryWholesale.Domain.Models.DBO
{
    [Table(nameof(Wholesaler))]
    [PrimaryKey(nameof(WholesalerID))]
    public class Wholesaler
    {
        public int WholesalerID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<WholesalerStock>? WholesalerStock { get; set; }
        public virtual ICollection<Sale>? Sales { get; set; }
    }
}

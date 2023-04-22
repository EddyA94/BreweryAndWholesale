using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryWholesale.Domain.Models.DBO
{
    [Table(nameof(Brewery))]
    [PrimaryKey(nameof(BrewerID))]
    public class Brewery
    {
        public int BrewerID { get; set; }
        public string Name { get; set; }
        public ICollection<Beer> Beers { get; set; }
    }
}

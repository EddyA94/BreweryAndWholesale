using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryWholesale.Domain.Models.DBO
{
    [Table(nameof(Beer))]
    [PrimaryKey(nameof(BeerID))]
    public class Beer
    {
        public int BeerID { get; set; }
        public string Name { get; set; }
        public float AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public int BreweryID { get; set; }
        public Brewery Brewery { get; set; }
    }
}

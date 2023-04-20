using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BreweryWholesale.Domain.Models.DTO
{
    public class Beer_Dto
    {
        [Required]
        public string BeerName { get; set; }
        [Required]
        public string BreweryName { get; set; }
        public float AlcoholContent { get; set; }
        public decimal Price { get; set; }
    }
}

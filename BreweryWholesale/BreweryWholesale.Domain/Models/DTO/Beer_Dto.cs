﻿using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Domain.Models.DTO
{
    public class Beer_Dto
    {
        [Required]
        public string BeerName { get; set; }
        [Required]
        public int BreweryId { get; set; }
        public float AlcoholContent { get; set; }
        public decimal Price { get; set; }
    }
}

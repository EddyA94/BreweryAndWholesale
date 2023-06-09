﻿using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Domain.Models.DTO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IBeerService
    {
        Task<IEnumerable<Beer>> GetAllBeersAsync();
        Task<IEnumerable<Beer>> GetBeerByIdsAsync(IEnumerable<int> beerId);
        Task AddBeerAsync(Beer_Dto beer_Dto);
        Task DeleteBeerAsync(int beerId);
    }
}

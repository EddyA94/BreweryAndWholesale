﻿using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository
{
    public interface IWholesalerStockRepository
    {
        Task<IEnumerable<WholesalerStock>> GetWholeSalerStockByIdandBeerIdAsync(int wholesalerId, List<int> beerId);

        Task UpsertWholeSalerStockAsync(WholesalerStock wholesalerStock);

        Task<WholesalerStock?> GetWholeSalerStockByStockIdAsync(int WholesalerStockId);
    }
}

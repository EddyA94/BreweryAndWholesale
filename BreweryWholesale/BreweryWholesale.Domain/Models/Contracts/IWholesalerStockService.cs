﻿using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IWholesalerStockService
    {
        Task<WholesalerStock?> GetWholeSalerStockByStockIdAsync(int WholesalerStockId);
        Task<WholesalerStock?> GetStockByWholesalerIdAndBeerIdAsync(int wholesalerId, int BeerId);

        Task UpsertWholesaleStockAsync(WholesalerStock wholesalerStock);
    }
}

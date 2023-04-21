using BreweryWholesale.Domain.Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IWholesalerStockService
    {
        Task<WholesalerStock?> GetStockByWholesalerIdAndBeerIdAsync(int wholesalerId, int BeerId);

        Task UpsertWholesaleStockAsync(WholesalerStock wholesalerStock);
    }
}

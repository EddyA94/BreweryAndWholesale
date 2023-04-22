using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;

namespace BreweryWholesale.Infrastructure.Services
{
    public class WholesalerServices : IWholesalerService
    {

        private readonly IWholesalerStockService _wholesalerStockService;

        public WholesalerServices(IWholesalerStockService wholesalerStockService)
        {
            _wholesalerStockService = wholesalerStockService;
        }

        public async Task UpdateWholesalerStockQuantityAsync(WholesalerStock_Dto wholesalerStock_Dto)
        {
            try
            {
                if (wholesalerStock_Dto.StockQuantity < 0) 
                {
                    throw new CustomExceptions("Quantity cannot be less than 0", (int)System.Net.HttpStatusCode.BadRequest);
                }
                var wholesalerStock = await _wholesalerStockService.GetWholeSalerStockByStockIdAsync(wholesalerStock_Dto.WholesalerStockId);
                if (wholesalerStock == null)
                {
                    throw new CustomExceptions("wholesalerStock Id Does not Exist", (int)System.Net.HttpStatusCode.NotFound);
                }
                wholesalerStock.StockQuantity = wholesalerStock_Dto.StockQuantity;
                await _wholesalerStockService.UpsertWholesaleStockAsync(wholesalerStock);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

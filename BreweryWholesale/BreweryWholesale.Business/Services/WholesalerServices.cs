using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;
using BreweryWholesale.Infrastructure.Repository;

namespace BreweryWholesale.Infrastructure.Services
{
    public class WholesalerServices : IWholesalerService
    {

        private readonly IWholesalerStockService _wholesalerStockService;
        private readonly IWholesalerRepository _wholesalerRepository;

        public WholesalerServices(IWholesalerStockService wholesalerStockService, IWholesalerRepository wholesalerRepository)
        {
            _wholesalerStockService = wholesalerStockService;
            _wholesalerRepository = wholesalerRepository;
        }

        public async Task<bool> IsWholesalerAvailableAsync(int wholesalerId)
        {
            try
            {
                return await _wholesalerRepository.IsWholesalerAvailableAsync(wholesalerId);
            }
            catch (Exception)
            {
                throw;
            }
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

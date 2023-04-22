
using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Infrastructure.Repository;

namespace BreweryWholesale.Infrastructure.Services
{
    public class WholesalerStockServices : IWholesalerStockService
    {
        private readonly IWholesalerStockRepository _wholesalerStockRepository;
        public WholesalerStockServices(IWholesalerStockRepository wholesalerStockRepository)
        {
            _wholesalerStockRepository = wholesalerStockRepository;
        }

        public async Task<WholesalerStock?> GetStockByWholesalerIdAndBeerIdAsync(int wholesalerId, int beerId)
        {
            return await _wholesalerStockRepository.GetWholeSalerStockByIdandBeerIdAsync(wholesalerId, beerId);
        }

        public async Task<WholesalerStock?> GetWholeSalerStockByStockIdAsync(int WholesalerStockId)
        {
            return await _wholesalerStockRepository.GetWholeSalerStockByStockIdAsync(WholesalerStockId);
        }

        public async Task UpsertWholesaleStockAsync(WholesalerStock wholesalerStock)
        {
            await _wholesalerStockRepository.UpsertWholeSalerStockAsync(wholesalerStock);
        }
    }
}

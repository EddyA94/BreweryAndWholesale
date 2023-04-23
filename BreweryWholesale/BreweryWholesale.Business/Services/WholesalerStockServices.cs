using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Infrastructure.Exceptions;
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

        public async Task<IEnumerable<WholesalerStock>> GetStockByWholesalerIdAndBeerIdAsync(int wholesalerId, List<int> beerId)
        {
            return await _wholesalerStockRepository.GetWholeSalerStockByIdandBeerIdAsync(wholesalerId, beerId);
        }

        public async Task<WholesalerStock> GetWholeSalerStockByStockIdAsync(int WholesalerStockId)
        {
            var wholesalerStock = await _wholesalerStockRepository.GetWholeSalerStockByStockIdAsync(WholesalerStockId);
            return wholesalerStock ?? throw new CustomExceptions("wholesalerStock Id Does not Exist", (int)System.Net.HttpStatusCode.NotFound);
        }

        public async Task UpsertWholesaleStockAsync(WholesalerStock wholesalerStock)
        {
            await _wholesalerStockRepository.UpsertWholeSalerStockAsync(wholesalerStock);
        }
    }
}

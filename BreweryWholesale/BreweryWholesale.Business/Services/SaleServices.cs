using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;
using BreweryWholesale.Infrastructure.Repository;
using BreweryWholesale.Infrastructure.UnitsOfWork;
using System.Drawing;

namespace BreweryWholesale.Infrastructure.Services
{
    public class SaleServices : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IWholesalerStockService _wholesalerStockService;
        private readonly ITransactionUnitOfWork _transactionUnitOfWork;

        public SaleServices(ISaleRepository saleRepository, IWholesalerStockService wholesalerStockService, ITransactionUnitOfWork transactionUnitOfWork)
        {
            _saleRepository = saleRepository;
            _wholesalerStockService = wholesalerStockService;
            _transactionUnitOfWork = transactionUnitOfWork;
        }

        public async Task UpsertSalesToWholesalerAsync(Sale_Dto sale_Dto)
        {
            try
            {
                Sale sale = new Sale()
                {
                    BeerID = sale_Dto.BeerId,
                    WholesalerID = sale_Dto.WholesalerId,
                    Quantity = sale_Dto.Quantity
                };
                int wholeSalerStockQuantity = 0;
                int wholesalerStockId = 0;
                await foreach (var wholeSalerStock in _wholesalerStockService.GetStockByWholesalerIdAndBeerIdAsync(sale_Dto.WholesalerId, new List<int> { sale_Dto.BeerId }))
                {
                    if (wholeSalerStock != null)
                    {
                        wholeSalerStockQuantity += wholeSalerStock.StockQuantity;
                        wholesalerStockId = wholeSalerStock.WholesalerStockID;
                    }
                }

                if (sale_Dto.Quantity <= 0)
                {
                    throw new CustomExceptions("Quantity cannot be equal or less than 0", (int)System.Net.HttpStatusCode.BadRequest);
                }

                WholesalerStock wholesalerStock = new WholesalerStock()
                {
                    WholesalerStockID = wholesalerStockId,
                    BeerID = sale_Dto.BeerId,
                    WholesalerID = sale_Dto.WholesalerId,
                    StockQuantity = sale_Dto.Quantity + wholeSalerStockQuantity,
                };
                await _transactionUnitOfWork.BeginTransactionAsync();

                try
                {
                    await _saleRepository.UpsertSalesToWholeSalerAsync(sale);
                    await _wholesalerStockService.UpsertWholesaleStockAsync(wholesalerStock);
                    await _transactionUnitOfWork.CommitTransactionAsync();
                }
                catch (Exception)
                {
                    await _transactionUnitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

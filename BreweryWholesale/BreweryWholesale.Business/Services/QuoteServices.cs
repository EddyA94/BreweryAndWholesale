using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;

namespace BreweryWholesale.Infrastructure.Services
{
    public class QuoteServices : IQuoteService
    {
        private readonly IWholesalerService _wholesalerService;
        private readonly IWholesalerStockService _wholesalerStockService;
        private readonly IBeerService _beerService;

        public QuoteServices(IWholesalerService wholesalerService, IWholesalerStockService wholesalerStockService, IBeerService beerService)
        {
            _wholesalerService = wholesalerService;
            _wholesalerStockService = wholesalerStockService;
            _beerService = beerService;
        }

        public async Task<QuoteResponse_Dto> RequestQuoteAsync(QuoteRequest_Dto quoteRequest_Dto)
        {
            //Validations
            await ValidateQuoteRequest(quoteRequest_Dto);

            var requestedBeerIds = quoteRequest_Dto.OrderItems.Select(S => S.BeerId).ToList();
            var requestedBeers = await _beerService.GetBeerByIdsAsync(quoteRequest_Dto.OrderItems.Select(S => S.BeerId).AsEnumerable());
            var wholesalerStocks = _wholesalerStockService.GetStockByWholesalerIdAndBeerIdAsync(quoteRequest_Dto.WholesalerId, requestedBeerIds);

            await ValidateWholesalerStock(requestedBeers, quoteRequest_Dto.OrderItems, wholesalerStocks, quoteRequest_Dto, requestedBeerIds);

            //if all validation passes
            decimal totalPrice = 0;
            foreach (var requestedBeer in quoteRequest_Dto.OrderItems)
            {
                totalPrice += requestedBeer.Quantity * requestedBeers.Where(W => W.BeerID == requestedBeer.BeerId).First().Price;
            }

            return ApplyDiscounts(totalPrice, quoteRequest_Dto);
        }

        private async Task ValidateWholesalerStock(IEnumerable<Beer> requestedBeers, IList<OrderItem_Dto> orderItems, IAsyncEnumerable<WholesalerStock> wholesalerStocks, QuoteRequest_Dto quoteRequest_Dto, List<int> requestedBeerIds)
        {
            //Check if all beers are in system
            var noneExistingBeers = requestedBeerIds.Except(requestedBeers.Select(S => S.BeerID));
            if (noneExistingBeers.Any())
            {
                throw new CustomExceptions("The following beers '" + string.Join(", ", noneExistingBeers.Select(S => S)) + "' do not exist in the system", (int)System.Net.HttpStatusCode.BadRequest);
            }

            var beersSoldByWholesaler = new List<int>();
            await foreach (var wholesalerStock in wholesalerStocks)
            {
                var requestedItem = quoteRequest_Dto.OrderItems.Where(W => W.BeerId == wholesalerStock.BeerID).FirstOrDefault();
                var beer = requestedBeers.Where(W => W.BeerID == wholesalerStock.BeerID).First();
                if (requestedItem != null)
                {
                    beersSoldByWholesaler.Add(wholesalerStock.BeerID);
                    var requestedQty = requestedItem.Quantity;
                    if (requestedQty > wholesalerStock.StockQuantity)
                    {
                        throw new CustomExceptions("Quantity ordered for " + beer.Name + " is greater than stock quantity, max quantity available is " + wholesalerStock.StockQuantity, (int)System.Net.HttpStatusCode.BadRequest);
                    }
                }
            }
            //6- Check if All beers are sold by wholesaler
            if (beersSoldByWholesaler.Count == 0)
            {
                throw new CustomExceptions("None Of the requested beers are sold by this wholesaler", (int)System.Net.HttpStatusCode.BadRequest);
            }
            var missingBeers = await _beerService.GetBeerByIdsAsync(requestedBeerIds.Except(beersSoldByWholesaler).AsEnumerable());
            if (missingBeers != null && missingBeers.Any())
            {
                throw new CustomExceptions("The following beers '" + string.Join(", ", missingBeers.Select(S => S.Name)) + "' are not sold by the wholesaler", (int)System.Net.HttpStatusCode.BadRequest);
            }

        }

        private QuoteResponse_Dto ApplyDiscounts(decimal totalPrice, QuoteRequest_Dto quoteRequest_Dto)
        {

            //A 20 % discount is applied above 20 drinks
            if (quoteRequest_Dto.OrderItems.Sum(S => S.Quantity) > 20)
            {
                return CalculateDiscountedPrice(totalPrice, 0.2m);
            }
            //A 10 % discount is applied above 10 drinks
            if (quoteRequest_Dto.OrderItems.Sum(S => S.Quantity) > 10)
            {
                return CalculateDiscountedPrice(totalPrice, 0.1m);
            }
            return new QuoteResponse_Dto()
            {
                Price = totalPrice,
                Summary = "no Discounts Applied, total price is " + totalPrice.ToString("0.00") + " (Excluding VAT)*"
            };
        }

        internal async Task ValidateQuoteRequest(QuoteRequest_Dto quoteRequest_Dto)
        {
            // Check if Quote is Filled
            if (quoteRequest_Dto == null)
            {
                throw new CustomExceptions("Invalid order", (int)System.Net.HttpStatusCode.BadRequest);
            }

            // Check if Order Is no empty
            if (quoteRequest_Dto.OrderItems == null || quoteRequest_Dto.OrderItems.Count == 0)
            {
                throw new CustomExceptions("Order Cannot Be empty", (int)System.Net.HttpStatusCode.BadRequest);
            }

            // Check if Wholesaler exists in DB
            if (!await _wholesalerService.IsWholesalerAvailableAsync(quoteRequest_Dto.WholesalerId))
            {
                throw new CustomExceptions("Wholsaler Does not Exist", (int)System.Net.HttpStatusCode.NotFound);
            }

            // Check if There are duplicated Items
            if (quoteRequest_Dto.OrderItems.GroupBy(x => x.BeerId).Any(g => g.Count() > 1))
            {
                throw new CustomExceptions("Duplicates in order", (int)System.Net.HttpStatusCode.BadRequest);
            }
        }

        internal static QuoteResponse_Dto CalculateDiscountedPrice(decimal totalPrice, decimal discount)
        {
            decimal disountedPrice = totalPrice - (totalPrice * discount);
            return new QuoteResponse_Dto()
            {
                Price = disountedPrice,
                Summary = "A " + discount * 100 + "% discount has been applied, total price is " + disountedPrice.ToString("0.00") + " (Excluding VAT)*"
            };
        }
    }
}

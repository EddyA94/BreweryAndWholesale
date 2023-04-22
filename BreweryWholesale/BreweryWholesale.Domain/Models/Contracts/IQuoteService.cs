using BreweryWholesale.Domain.Models.DTO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IQuoteService
    {
        Task<QuoteResponse_Dto> RequestQuoteAsync(QuoteRequest_Dto quoteRequest_Dto); 
    }
}
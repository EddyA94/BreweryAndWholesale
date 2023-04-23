using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;
using BreweryWholesale.Infrastructure.Services;
using Moq;

namespace BreweryWholesale.UnitTests.QuoteServicesTest
{
    public class QuoteServicesTest
    {
        private QuoteServices _quoteServices;
        private Mock<IWholesalerService> _wholesalerServiceMock;
        private Mock<IWholesalerStockService> _wholesalerStockServiceMock; 
        private Mock<IBeerService> _beerServiceMock;

        [SetUp]
        public void Setup()
        {
            _wholesalerServiceMock = new Mock<IWholesalerService>();
            _wholesalerStockServiceMock = new Mock<IWholesalerStockService>();
            _beerServiceMock = new Mock<IBeerService>();

            _quoteServices = new QuoteServices(_wholesalerServiceMock.Object, _wholesalerStockServiceMock.Object, _beerServiceMock.Object);
        }

        [Test]
        [TestCase(100, 10)]
        [TestCase(100, 20)]
        public void DiscountCalculator(decimal totalPrice, decimal discount)
        {
            //Arange
            decimal disountedPrice = totalPrice - totalPrice * discount;

            //Act
            QuoteResponse_Dto quoteResponse_Dto = QuoteServices.CalculateDiscountedPrice(totalPrice, discount);

            //Assert
            Assert.That(quoteResponse_Dto.Price, Is.EqualTo(disountedPrice));
        }

        [Test]
        [TestCase(null)]
        public void ValidateQuoteRequest_Throws_CustomException_When_QuoteRequestDto_Is_Invalid(QuoteRequest_Dto quoteRequest_Dto)
        {
            //Act & Assert
            Assert.ThrowsAsync<CustomExceptions>(async () => await _quoteServices.ValidateQuoteRequest(quoteRequest_Dto));
        }
    }
}
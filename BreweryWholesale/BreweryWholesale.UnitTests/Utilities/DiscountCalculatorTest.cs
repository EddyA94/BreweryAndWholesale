using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Services;

namespace BreweryWholesale.UnitTests.Utilities
{
    public class DiscountCalculatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(100, 10)]
        [TestCase(100, 20)]
        public void Test1(decimal totalPrice, decimal discount)
        {
            //Arange
            decimal disountedPrice = totalPrice - totalPrice * discount;

            //Act
            QuoteResponse_Dto quoteResponse_Dto = QuoteServices.CalculateDiscountedPrice(totalPrice, discount);

            //Assert
            Assert.That(quoteResponse_Dto.Price, Is.EqualTo(disountedPrice));
        }
    }
}
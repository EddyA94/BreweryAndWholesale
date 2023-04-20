using BreweryWholesale.Domain.Models.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerService;
        public BeerController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet]
        [Route("GetAllBeers")]
        public async Task<ActionResult> GetAllBeers()
        {
            var result = await _beerService.GetAllBeersAsync();
            return Ok(result);
        }
    }
}

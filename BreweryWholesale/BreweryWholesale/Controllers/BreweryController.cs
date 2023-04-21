using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : Controller
    {
        private readonly IBreweryService _breweryService;
        public BreweryController(IBreweryService breweryService)
        {
            _breweryService = breweryService;
        }

        [HttpGet]
        [Route("GetBeersByBreweryName")]
        public async Task<ActionResult> GetBeersByBreweryName([FromQuery][Required] string BreweryName)
        {
            try
            {
                var result = await _breweryService.GetAllBeersByBreweryNameAsync(BreweryName);
                if (result != null && result.Beers.Count == 0)
                {
                    throw new CustomExceptions("Brewery Do not have any beers", (int)System.Net.HttpStatusCode.NotFound);
                }
                if (result == null)
                {
                    return NotFound("No Beers Exists for brewery or brewery does not exist");
                }
                return Ok(result);
            }
            catch (CustomExceptions ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerService;
        private readonly IBreweryService _breweryService;
        public BeerController(IBeerService beerService, IBreweryService breweryService)
        {
            _beerService = beerService;
            _breweryService = breweryService;
        }

        [HttpGet]
        [Route("GetAllBeers")]
        public async Task<ActionResult> GetAllBeers()
        {
            var result = await _beerService.GetAllBeersAsync();
            return Ok(result);
        }


        [HttpPost]
        [Route("AddNewBeer")]
        public async Task<IActionResult> AddNewBeer([FromBody] Beer_Dto beer_Dto)
        {
            if (beer_Dto.BreweryName == string.Empty || beer_Dto.BeerName == string.Empty || beer_Dto.Price == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Missing Fields required (Brewery Name or Beer Name or Price)");
            }

            try
            {
                await _beerService.AddBeerAsync(beer_Dto);
                return Ok();
            }
            catch (CustomExceptions ex)
            {
                return StatusCode(ex.StatusCode,ex.Message );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteBeerById")]
        public async Task<IActionResult> DeleteBeerById([FromQuery][Required] int beerId)
        {
            try
            {
                await _beerService.DeleteBeerAsync(beerId);
                return Ok();
            }
            catch (CustomExceptions ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesale.Api.Controllers
{
    public class WholesalerController : Controller
    {
        private readonly IWholesalerService _wholesalerService;

        public WholesalerController(IWholesalerService wholesalerService)
        {
            _wholesalerService = wholesalerService;
        }

        [HttpPost]
        [Route("UpdateWholesalerStockQuantity")]
        public async Task<ActionResult> UpdateWholesalerStockQuantity([FromBody] WholesalerStock_Dto wholesalerStock_Dto)
        {
            try
            {
                await _wholesalerService.UpdateWholesalerStockQuantityAsync(wholesalerStock_Dto);
                return Ok();
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

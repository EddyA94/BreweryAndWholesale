using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly ISaleService _saleService;
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        [Route("UpsertSalesToWolesaler")]
        public async Task<ActionResult> UpsertSalesToWolesaler([FromBody] Sale_Dto sale_Dto)
        {
            try
            {
                await _saleService.UpsertSalesToWholesalerAsync(sale_Dto);
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

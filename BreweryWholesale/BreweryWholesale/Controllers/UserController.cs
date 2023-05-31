using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController :  ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<ActionResult> LoginUser([FromBody][Required] User_Dto user_Dto)
        {
            var result = await _userService.LoginUser(user_Dto);
            Response.Headers.Add("Token", result);
            return Ok();
        }
    }
}

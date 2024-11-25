using Microsoft.AspNetCore.Mvc;
using RapidPayAPI.Models;
using RapidPayAPI.Services.Interfaces;

namespace RapidPayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> logger;
        private readonly IUserService userService;
        public AuthorizationController(ILogger<AuthorizationController> logger, IUserService userService) 
        {
            this.logger = logger;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetToken([FromForm] UserTokenRequest user)
        {
            try
            {
                return Ok(userService.GetToken(user));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

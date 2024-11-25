using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayAPI.Exceptions;
using RapidPayAPI.Models;
using RapidPayAPI.Services;
using RapidPayAPI.Services.Interfaces;

namespace RapidPayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> logger;
        private readonly ICardService cardService;

        public CardController(ILogger<CardController> logger, ICardService cardService)
        { 
            this.logger = logger;
            this.cardService = cardService;
        }

        [HttpGet("{id}")]
        public IActionResult GetCardBalance(int id)
        {
            try
            {
                return Ok(cardService.GetBalance(id));
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Pay(PayRequest payRequest)
        {
            try
            {
                return Ok(cardService.Pay(payRequest));
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult CreateCard(CreateCardRequest createCardRequest)
        {
            try
            {
                return Ok(cardService.CreateCard(createCardRequest));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

using BusinessLayer;
using BusinessLayer.Model;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Orderlytics_Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemOrdersController : ControllerBase
    {
        private readonly FoodItemOrdersRepository _junctionRepo;
        private readonly ILogger<FoodItem> _logger;

        public FoodItemOrdersController(FoodItemOrdersRepository junctionRepo, ILogger<FoodItem> logger)
        {
            this._junctionRepo = junctionRepo;
            this._logger = logger;
        }

        [HttpPost("AddFoodItem")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult AddFoodItem([FromBody] FoodItemOrder orderDetails)
        {
            FoodItemOrder responseItem = _junctionRepo.Add(orderDetails);

            if (responseItem != null)
            {
                return Ok(responseItem);

            }
            return StatusCode(500, "Error occured while updating order details");
        }

    }
}

using BusinessLayer;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Orderlytics_Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodItemsRepository _foodItemRepository;
        private readonly ILogger<FoodItem> _logger;

        public FoodItemsController(IFoodItemsRepository foodItemRepository, ILogger<FoodItem> logger)
        {
            this._foodItemRepository = foodItemRepository;
            this._logger = logger;
        }

        [HttpGet("GetAllFoodItems")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAllFoodItems()
        {
            IList<FoodItem> foodItems = _foodItemRepository.GetAllFoodItems();
            if (foodItems.Count == 0)
            {
                _logger.LogInformation("Food items not found");
                return NotFound("Food items not found");
            }
            _logger.LogInformation("Food items found : Response 200 Ok");
            return Ok(foodItems);
        }

        [HttpGet("GetFoodItemById/{foodId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetFoodItemById(string foodId)
        {
            FoodItem foodItem = _foodItemRepository.GetFoodItemById(foodId);
            if (foodItem == null)
            {
                _logger.LogInformation("Food items not found");
                return NotFound("Food items not found");
            }
            _logger.LogInformation("Food items found : Response 200 Ok");
            return Ok(foodItem);
        }

        [HttpGet("GetFoodItemsByCategory/{category}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetFoodItemsByCategory(string category)
        {
            IList<FoodItem> foodItems = _foodItemRepository.GetFoodItemsByCategory(category);
            if (foodItems.Count == 0)
            {
                _logger.LogInformation("Food items not found for category " + category);
                return NotFound("Food items not found");
            }
            _logger.LogInformation("Food items found : Response 200 Ok");
            return Ok(foodItems);
        }

        [HttpGet("GetFoodItemByName/{name}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetFoodItemByName(string name)
        {
            FoodItem foodItem = _foodItemRepository.GetFoodItemByName(name);
            if (foodItem == null)
            {
                _logger.LogInformation("Server error occured");
                return StatusCode(500, "Internal Server Error");
            }
            _logger.LogInformation("Food items found : Response 200 Ok");
            return Ok(foodItem);
        }

        [HttpGet("GetFoodItemPrice/{foodId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetFoodItemPrice(string foodId)
        {
            double price = _foodItemRepository.GetFoodItemPrice(foodId);
            if (price == 0)
            {
                return NotFound("Price not found for food item with id " + foodId);
            }
            _logger.LogInformation("Food item price found : Response 200 Ok");
            return Ok(price);
        }

        [HttpGet("GetNumberOfFoodItems")]
        public IActionResult GetNumberOfFoodItems()
        {
            int nItems = _foodItemRepository.GetNumberOfFoodItems();
            return Ok(nItems);
        }


        [HttpPost("AddFoodItem")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult AddFoodItem([FromBody] FoodItem foodItem)
        {
            _logger.LogInformation("Performing model validation");
            if (ModelState.IsValid != true)
            {
                _logger.LogError("Food item details found invalid");
                return BadRequest("Invalid food item details");
            }
            FoodItem responseItem = _foodItemRepository.AddFoodItem(foodItem);

            if (responseItem == null)
            {
                return StatusCode(500, "Error occured while adding food items");
            }
            return Ok(responseItem);
        }


        [HttpPut("UpdateFoodItem/{foodId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateFoodItem(string foodId, [FromBody] FoodItem foodItem)
        {
            if (ModelState.IsValid != true)
            {
                _logger.LogError("Food item details found invalid");
                return BadRequest("Invalid food item details");
            }

            FoodItem responseItem = _foodItemRepository.UpdateFoodItem(foodId, foodItem);

            if (responseItem != null)
            {
                return Ok(responseItem);
            }
            else
            {
                FoodItem item = _foodItemRepository.GetFoodItemById(foodId);
                if (item == null)
                {
                    return BadRequest("Food item not found with id " + foodId);
                }
                else
                {
                    return StatusCode(500, "Error occured while updating food item details");
                }
            }
        }


        [HttpDelete("DeleteFoodItem/{foodId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteFoodItem(string foodId)
        {
            FoodItem responseItem = _foodItemRepository.DeleteFoodItem(foodId);

            if (responseItem != null)
            {
                return Ok(responseItem);
            }
            else
            {
                FoodItem foodItem = _foodItemRepository.GetFoodItemById(foodId);
                if (foodItem == null)
                {
                    return BadRequest("Food item not found with id " + foodId);
                }
                else
                {
                    return StatusCode(500, "Error occured while deleting food item");
                }
            }
        }
    }
}

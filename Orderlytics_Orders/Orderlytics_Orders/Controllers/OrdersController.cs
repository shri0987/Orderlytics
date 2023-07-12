using BusinessLayer;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Orderlytics_Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrdersRepository ordersRepo;
        public OrdersController(IOrdersRepository ordersRepo)
        {
            this.ordersRepo = ordersRepo;
        }

        [HttpGet("GetAllOrders")]
        public IActionResult Get()
        {
            ICollection<Order> orders = ordersRepo.GetAllOrders();
            if (orders.Count == 0)
            {
                return NotFound("Orders not found");
            }
            return Ok(orders);
        }

        [HttpGet("GetOrderById/{orderId}")]
        public IActionResult GetOrderById(string orderId)
        {
            Order order = ordersRepo.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound("Order not found with ID " + orderId);
            }
            return Ok(order);
        }


        [HttpPost("AddOrder")]
        public IActionResult Post([FromBody] Order order)
        {
            Order responseOrder = ordersRepo.AddOrder(order);
            if (responseOrder!=null)
            {
                return Ok(responseOrder);
            }
            return BadRequest("Order not placed");
        }



        [HttpDelete("DeleteCustomer/{customerId}")]
        public IActionResult Delete(string orderId)
        {
            Order responseOrder = ordersRepo.DeleteOrder(orderId);
            if (responseOrder != null)
            {
                return Ok(responseOrder);
            }
            return NotFound("Customer not found");
        }

    }
}

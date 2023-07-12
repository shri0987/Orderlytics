using BusinessLayer;
using BusinessLayer.Model;
using Microsoft.Extensions.Logging;
#pragma warning disable CS8600, CS8603

namespace DataAccessLayer
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OrdersDbContext _db;
        private readonly ILogger<FoodItem> _logger;
        public OrdersRepository(OrdersDbContext db, ILogger<FoodItem> logger)
        {
            this._db = db;
            this._logger = logger;
        }

        public IList<Order> GetAllOrders()
        {
            _logger.LogInformation("Retrieving order details");
            IList<Order> orders = _db.Orders.ToList();
            return orders;
        }

        public Order GetOrderById(string orderId)
        {
            _logger.LogInformation("Retrieving order details");
            Order order = _db.Orders.Find(orderId);
            return order;
        }

        public IList<Order> GetOrdersByCustomerId(string customerId)
        {
            _logger.LogInformation("Fetching orders with customer id " + customerId);
            IList<Order> orders = _db.Orders.Where(c => c.CustomerId == customerId).ToList();
            return orders;
        }

        public int GetNumberOfOrders()
        {
            _logger.LogInformation("Fetching number of orders");
            int nOrders = _db.Orders.Select(o => o.OrderId).Distinct().Count();
            return nOrders;
        }

        public Order AddOrder(Order order)
        {
            DateTime timestamp = DateTime.Now;
            order.OrderDateTime= timestamp;

            try
            {
                _logger.LogInformation("Placing order...");
                _db.Add(order);
                if (_db.SaveChanges() > 0)
                {
                    return order;
                }

                _logger.LogError("order not placed");
                return new Order();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while placing order " + ex.Message);
                return new Order();
            }
        }

        public Order DeleteOrder(string id)
        {
            Order order = GetOrderById(id);
            if (order != null)
            {
                _logger.LogInformation("Order details found for " + id);
                try
                {
                    _db.Remove(order);
                    if (_db.SaveChanges() > 0)
                    {
                        _logger.LogInformation("Order deleted successfully ");
                        return order;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured while performing delete operation " + ex.Message);
                    return new Order();
                }
            }
            _logger.LogError("Order details not found");
            return new Order();
        }

    }
}
using BusinessLayer.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FoodItemOrdersRepository
    {
        private readonly OrdersDbContext _db;
        private readonly ILogger<FoodItem> _logger;
        public FoodItemOrdersRepository(OrdersDbContext db, ILogger<FoodItem> logger)
        {
            this._db = db;
            this._logger = logger;
        }
      
        public FoodItemOrder Add(FoodItemOrder orderDetails)
        {
            try
            {
                _logger.LogInformation("Adding food item details...");
                _db.FoodItemOrders.Add(orderDetails);
                if (_db.SaveChanges() > 0)
                {
                    return orderDetails;
                }

                _logger.LogError("Food item not added");
                return new FoodItemOrder();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while adding food item " + ex.Message);
                return new FoodItemOrder();
            }
        }
    }
}

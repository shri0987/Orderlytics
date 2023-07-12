using BusinessLayer;
using BusinessLayer.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8600, CS8603, CS8602

namespace DataAccessLayer
{
    public class FoodItemsRepository : IFoodItemsRepository
    {
        private readonly OrdersDbContext _db;
        private readonly ILogger<FoodItem> _logger;
        public FoodItemsRepository(OrdersDbContext db, ILogger<FoodItem> logger)
        {
            this._db = db;
            this._logger = logger;
        }

        public IList<FoodItem> GetAllFoodItems()
        {
            _logger.LogInformation("Retrieving food items");
            IList<FoodItem> foodItems = _db.FoodItems.ToList();
            return foodItems;
        }

        public IList<FoodItem> GetFoodItemsByCategory(string category)
        {
            _logger.LogInformation("Retrieving items for category : " + category);
            IList<FoodItem> foodItems = _db.FoodItems.Where(f => f.ItemCategory == category).ToList();
            return foodItems;
        }

        public FoodItem GetFoodItemByName(string name)
        {
            try
            {
                _logger.LogInformation("Retrieving item with name : " + name);
                FoodItem foodItem = _db.FoodItems.Where(f => f.ItemName == name).FirstOrDefault();
                return foodItem;
            }
            catch (Exception ex)
            {
                return new FoodItem();
            }
        }

        public FoodItem GetFoodItemById(string id)
        {
            _logger.LogInformation("Finding food item with id " + id);
            FoodItem foodItem = _db.FoodItems.Where(f => f.FoodId == id).FirstOrDefault();
            return foodItem;
        }

        public double GetFoodItemPrice(string id)
        {
            _logger.LogInformation("Finding price of food item with id " + id);
            double price = _db.FoodItems.Find(id).ItemPrice;
            return price;
        }

        public int GetNumberOfFoodItems()
        {
            _logger.LogInformation("Fetching number of food items");
            int nItems = _db.FoodItems.Select(c => c.FoodId).Distinct().Count();
            return nItems;
        }

        public FoodItem AddFoodItem(FoodItem food)
        {
            try
            {
                _logger.LogInformation("Adding food item...");
                _db.FoodItems.Add(food);
                if (_db.SaveChanges() > 0)
                {
                    return food;
                }

                _logger.LogError("Food item not added");
                return new FoodItem();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while adding food item " + ex.Message);
                return new FoodItem();
            }
        }

        public FoodItem UpdateFoodItem(string id, FoodItem food)
        {
            _logger.LogInformation("Finding food item with id " + id);

            FoodItem foodItem = GetFoodItemById(id);

            if (foodItem != null)
            {
                _logger.LogInformation("Updating food details...");
                try
                {
                    foodItem.ItemName = food.ItemName.Trim();
                    foodItem.ItemCategory = food.ItemCategory.Trim();
                    foodItem.ItemDescription = food.ItemDescription.Trim();
                    foodItem.ItemPrice = food.ItemPrice;
                    foodItem.IsAvailable = food.IsAvailable;

                    if (_db.SaveChanges() > 0)
                    {
                        _logger.LogInformation("Food item details updated successfully");
                        return foodItem;
                    }
                    else
                    {
                        _logger.LogError("Error occured while updating food item details ");
                        return new FoodItem();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured while updating food item details " + ex.Message);
                    return new FoodItem();
                }
            }
            _logger.LogInformation("Food item not found");
            return new FoodItem();
        }

        public FoodItem DeleteFoodItem(string id)
        {
            FoodItem foodItem = GetFoodItemById(id);
            if (foodItem != null)
            {
                _logger.LogInformation("Food item details found for " + id);
                try
                {
                    _db.FoodItems.Remove(foodItem);
                    if (_db.SaveChanges() > 0)
                    {
                        _logger.LogInformation("Food item deleted successfully ");
                        return foodItem;
                    }
                    else
                    {
                        _logger.LogError("Error occured while performing delete operation");
                        return new FoodItem();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured while performing delete operation " + ex.Message);
                    return new FoodItem();
                }
            }
            _logger.LogError("Food item not found");
            return new FoodItem();
        }
    }
}

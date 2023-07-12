using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer
{
    public interface IFoodItemsRepository
    {
        IList<FoodItem> GetAllFoodItems();
        FoodItem GetFoodItemById(string id);
        FoodItem GetFoodItemByName(string foodName);
        FoodItem AddFoodItem(FoodItem item);
        FoodItem UpdateFoodItem(string foodId, FoodItem item);
        FoodItem DeleteFoodItem(string foodId);
        IList<FoodItem> GetFoodItemsByCategory(string categoryName);
        double GetFoodItemPrice(string foodId);
        int GetNumberOfFoodItems();
    }
}

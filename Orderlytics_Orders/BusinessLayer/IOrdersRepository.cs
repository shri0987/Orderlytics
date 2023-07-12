using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IOrdersRepository
    {
        IList<Order> GetAllOrders();
        Order GetOrderById(string id);
        IList<Order> GetOrdersByCustomerId(string customerId);
        int GetNumberOfOrders();
        Order AddOrder(Order obj);
        Order DeleteOrder(string id);
    }
}

using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ICustomerRepository<T> : IRepository<T> where T : class
    {
        int GetNumberOfCustomers();
        Customer GetByPhoneNumber(long phoneNumber);
    }
}

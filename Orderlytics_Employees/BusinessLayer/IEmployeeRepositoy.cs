using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IEmployeeRepository<T> : IRepository<T> where T : class
    {
        int GetNumberOfEmployees();
        Employee GetByPhoneNumber(long phoneNumber);
    }
}

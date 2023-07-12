using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(string id);
        bool Add(T obj);
        Customer Update(string id, T obj);
        Customer Delete(string id);
    }
}

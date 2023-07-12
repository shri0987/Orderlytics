using BusinessLayer;
using BusinessLayer.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8600
#pragma warning disable CS8603

namespace DataAccessLayer
{
    public class CustomerRepository : ICustomerRepository<Customer>
    {
        private readonly CustomerDbContext _db;
        private readonly ILogger<Customer> _logger;

        // Dependency Injection
        public CustomerRepository(CustomerDbContext db, ILogger<Customer> logger)
        {
            this._db = db;
            this._logger = logger;
        }

        public IList<Customer> GetAll()
        {
            _logger.LogInformation("Fetching all customer");
            IList<Customer> customers = _db.Customers.ToList();
            return customers;
        }

        public Customer GetById(string id)
        {
            _logger.LogInformation("Fetching customer with id " + id);
            Customer customer = _db.Customers.Find(id);
            return customer;
        }

        public Customer GetByPhoneNumber(long phoneNumber)
        {
            _logger.LogInformation("Fetching customer with phone number " + phoneNumber);
            Customer customer = _db.Customers.Where(c => c.CustomerPhoneNumber == phoneNumber).FirstOrDefault();
            return customer;
        }

        public int GetNumberOfCustomers()
        {
            _logger.LogInformation("Fetching number of customers");
            int nCustomers = _db.Customers.Select(c => c.CustomerId).Distinct().Count();
            return nCustomers;
        }

        public bool Add(Customer obj)
        {
            _logger.LogInformation("Adding customer...");

            try
            {
                _db.Customers.Add(obj);
                if (_db.SaveChanges() > 0)
                {
                    _logger.LogInformation("Customer added successfully");
                    return true;
                }
                _logger.LogInformation("Customer not added");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while adding customer " + ex.Message);
                return false;
            }
        }

        public Customer Update(string id, Customer obj)
        {
            _logger.LogInformation("Finding customer with id " + id);
            Customer customer = GetById(id);
            if (customer != null)
            {
                _logger.LogInformation("Updating customer details...");
                try
                {
                    customer.CustomerFirstName = obj.CustomerFirstName.Trim();
                    customer.CustomerLastName = obj.CustomerLastName.Trim();
                    customer.CustomerEmail = obj.CustomerEmail.Trim();
                    customer.CustomerPhoneNumber = obj.CustomerPhoneNumber;

                    if (_db.SaveChanges() > 0)
                    {
                        _logger.LogInformation("Customer details updated successfully");
                        return customer;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured while updating customer details " + ex.Message);
                    return new Customer();
                }
            }
            _logger.LogError("Customer not found");
            return new Customer();
        }

        public Customer Delete(string id)
        {
            Customer customer = GetById(id);
            if (customer != null)
            {
                _logger.LogInformation("Customer details found for " + id);
                try
                {
                    _db.Customers.Remove(customer);
                    if (_db.SaveChanges() > 0)
                    {
                        _logger.LogInformation("Customer deleted successfully ");
                        return customer;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured while performing delete operation " + ex.Message);
                    return new Customer();
                }
            }
            _logger.LogError("Customer not found");
            return new Customer();
        }

    }
}

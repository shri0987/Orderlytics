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
    public class EmployeeRepository : IEmployeeRepository<Employee>
    {
        private readonly EmployeeDbContext _db;
        private readonly ILogger<Employee> _logger;

        // Dependency Injection
        public EmployeeRepository(EmployeeDbContext db, ILogger<Employee> logger)
        {
            this._db = db;
            this._logger = logger;
        }

        public IList<Employee> GetAll()
        {
            _logger.LogInformation("Fetching all employee");
            IList<Employee> employees = _db.Employees.ToList();
            return employees;
        }

        public Employee GetById(string id)
        {
            _logger.LogInformation("Fetching employee with id " + id);
            Employee employee = _db.Employees.Find(id);
            return employee;
        }

        public Employee GetByPhoneNumber(long phoneNumber)
        {
            _logger.LogInformation("Fetching employee with phone number " + phoneNumber);
            Employee employee = _db.Employees.Where(c => c.EmployeePhoneNumber == phoneNumber).Single();
            return employee;
        }

        public int GetNumberOfEmployees()
        {
            _logger.LogInformation("Fetching number of employees");
            int nEmployees = _db.Employees.Select(c => c.EmployeeId).Distinct().Count();
            return nEmployees;
        }

        public bool Add(Employee obj)
        {
            _logger.LogInformation("Adding employee...");

            try
            {
                _db.Employees.Add(obj);
                if (_db.SaveChanges() > 0)
                {
                    _logger.LogInformation("Employee added successfully");
                    return true;
                }
                _logger.LogInformation("Employee not added");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while adding employee " + ex.Message);
                return false;
            }
        }

        public Employee Update(string id, Employee obj)
        {
            _logger.LogInformation("Finding employee with id " + id);
            Employee employee = GetById(id);
            if (employee != null)
            {
                _logger.LogInformation("Updating employee details...");
                try
                {
                    employee.EmployeeFirstName = obj.EmployeeFirstName.Trim();
                    employee.EmployeeLastName = obj.EmployeeLastName.Trim();
                    employee.EmployeeEmail = obj.EmployeeEmail.Trim();
                    employee.EmployeePhoneNumber = obj.EmployeePhoneNumber;

                    if (_db.SaveChanges() > 0)
                    {
                        _logger.LogInformation("Employee details updated successfully");
                        return employee;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured while updating employee details " + ex.Message);
                    return new Employee();
                }
            }
            _logger.LogError("Employee not found");
            return new Employee();
        }

        public Employee Delete(string id)
        {
            Employee employee = GetById(id);
            if (employee != null)
            {
                _logger.LogInformation("Employee details found for " + id);
                try
                {
                    _db.Employees.Remove(employee);
                    if (_db.SaveChanges() > 0)
                    {
                        _logger.LogInformation("Employee deleted successfully ");
                        return employee;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured while performing delete operation " + ex.Message);
                    return new Employee();
                }
            }
            _logger.LogError("Employee not found");
            return new Employee();
        }

    }
}

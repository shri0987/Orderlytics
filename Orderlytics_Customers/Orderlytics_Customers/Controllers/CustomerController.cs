using BusinessLayer.Model;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Azure;

namespace Orderlytics_Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository<Customer> custRepo;
        public CustomerController(ICustomerRepository<Customer> custRepo)
        {
            this.custRepo = custRepo;
        }

        [HttpGet("GetAllCustomers")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            ICollection<Customer> customers = custRepo.GetAll();
            if (customers.Count == 0)
            {
                return NotFound("Customers not found");
            }
            return Ok(customers);
        }

        [HttpGet("GetCustomerById/{customerId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerById(string customerId)
        {
            Customer customer = custRepo.GetById(customerId);
            if (customer == null)
            {
                return NotFound("Customer not found with ID " + customerId);
            }
            return Ok(customer);
        }


        [HttpGet("GetCustomerByPhoneNumber/{phoneNumber}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerByPhoneNumber(long phoneNumber)
        {
            Customer customer = custRepo.GetByPhoneNumber(phoneNumber);
            if (customer == null)
            {
                return NotFound("Customer not found with phone number " + phoneNumber);
            }
            return Ok(customer);
        }


        [HttpGet("GetNumberOfCustomers")]
        public IActionResult GetNumberOfCustomers()
        {
            int nCustomer = custRepo.GetNumberOfCustomers();
            return Ok(nCustomer);
        }

        [HttpPost("AddCustomer")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Customer customer)
        {
            string prefix = "CUST";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            customer.CustomerId = prefix + timestamp;


            if (ModelState.IsValid != true)
            {
                return BadRequest("Invalid customer details");
            }
            bool response = custRepo.Add(customer);

            if (response == false)
            {
                return StatusCode(500, "Error occured while adding customer");
            }
            return Ok(customer);
        }


        [HttpPut("UpdateCustomer/{customerId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCustomer(string customerId, [FromBody] Customer customer)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest("Invalid customer details");
            }

            Customer responseCustomer = custRepo.Update(customerId, customer);

            if (responseCustomer != null)
            {
                return Ok(responseCustomer);
            }
            else
            {
                Customer cust = custRepo.GetById(customerId);
                if (cust == null)
                {
                    return BadRequest("Customer not found with id " + customerId);
                }
                else
                {
                    return StatusCode(500, "Error occured while updating customer details");
                }
            }
        }


        [HttpDelete("DeleteCustomer/{customerId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string customerId)
        {
            Customer responseCustomer = custRepo.Delete(customerId);

            if (responseCustomer != null)
            {
                return Ok(responseCustomer);
            }
            else
            {
                Customer cust = custRepo.GetById(customerId);
                if (cust == null)
                {
                    return BadRequest("Customer not found with id " + customerId);
                }
                else
                {
                    return StatusCode(500, "Error occured while deleting customer");
                }
            }
        }
    }
}

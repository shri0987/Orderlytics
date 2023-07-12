using BusinessLayer.Model;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Azure;

namespace Orderlytics_Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository<Employee> empRepo;
        public EmployeeController(IEmployeeRepository<Employee> empRepo)
        {
            this.empRepo = empRepo;
        }

        [HttpGet("GetAllEmployees")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            ICollection<Employee> employees = empRepo.GetAll();
            if (employees.Count == 0)
            {
                return NotFound("Employees not found");
            }
            return Ok(employees);
        }

        [HttpGet("GetEmployeeById/{employeeId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetEmployeeById(string employeeId)
        {
            Employee employee = empRepo.GetById(employeeId);
            if (employee == null)
            {
                return NotFound("Employee not found with ID " + employeeId);
            }
            return Ok(employee);
        }


        [HttpGet("GetEmployeeByPhoneNumber/{phoneNumber}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetEmployeeByPhoneNumber(long phoneNumber)
        {
            Employee employee = empRepo.GetByPhoneNumber(phoneNumber);
            if (employee == null)
            {
                return NotFound("Employee not found with phone number " + phoneNumber);
            }
            return Ok(employee);
        }


        [HttpGet("GetNumberOfEmployees")]
        public IActionResult GetNumberOfEmployees()
        {
            int nEmployee = empRepo.GetNumberOfEmployees();
            return Ok(nEmployee);
        }

        [HttpPost("AddEmployee")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Employee employee)
        {
            string prefix = "EMP";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            employee.EmployeeId = prefix + timestamp;


            if (ModelState.IsValid != true)
            {
                return BadRequest("Invalid employee details");
            }
            bool response = empRepo.Add(employee);

            if (response == false)
            {
                return StatusCode(500, "Error occured while adding employee");
            }
            return Ok(employee);
        }


        [HttpPut("UpdatenEmployee/{employeeId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatenEmployee(string employeeId, [FromBody] Employee employee)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest("Invalid employee details");
            }

            Employee responsenEmployee = empRepo.Update(employeeId, employee);

            if (responsenEmployee != null)
            {
                return Ok(responsenEmployee);
            }
            else
            {
                Employee emp = empRepo.GetById(employeeId);
                if (emp == null)
                {
                    return BadRequest("Employee not found with id " + employeeId);
                }
                else
                {
                    return StatusCode(500, "Error occured while updating employee details");
                }
            }
        }


        [HttpDelete("DeleteEmployee/{employeeId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string employeeId)
        {
            Employee responseEmployee = empRepo.Delete(employeeId);

            if (responseEmployee != null)
            {
                return Ok(responseEmployee);
            }
            else
            {
                Employee emp = empRepo.GetById(employeeId);
                if (emp == null)
                {
                    return BadRequest("Employee not found with id " + employeeId);
                }
                else
                {
                    return StatusCode(500, "Error occured while deleting employee");
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get_all_employees")]
        public async Task<ActionResult<IEnumerable<employeeModel>>> Getemployees()
        {
            var employee = await _context.employees.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                EmployeeDetails = employee
            });

        }

        [HttpGet("get_employee/{id}")]
        public IActionResult Getemployee(int id)
        {
            var employee= _context.employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Employee Not Found"
                });
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    EmployeeDetails = employee
                });
            }

        }

        [HttpPost("add_employee")]
        public IActionResult Addemployee(employeeModel employeeobj)
        {
            if (employeeobj == null)
                return BadRequest();

            _context.employees.Add(employeeobj);
            _context.SaveChanges();
            return Ok(new
            {
                StatusCode = 200,
                Message = "employee added Successfully"
            });
        }

        [HttpPut("update_employee")]
        public IActionResult UpdateEmployee(employeeModel employeeobj)
        {
            if (employeeobj == null)
                return BadRequest();

            var employee = _context.employees.AsNoTracking().FirstOrDefault(x => x.Id == employeeobj.Id);
            if(employee == null)
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Employee Not Found"
                });
            else
            {
                _context.Entry(employeeobj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employee details updated Successfully"
                });
            }

        }

        [HttpDelete("delete_employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
                return NotFound(new
                {
                    StatusCode = 404,
                    Message="employee not found"

                });
            _context.employees.Remove(employee);
            _context.SaveChanges();
            return Ok(new
            {
                StatusCode=200,
                Message="employee deleted"
            });
        }
    }


}


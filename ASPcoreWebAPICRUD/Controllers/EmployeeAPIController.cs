using ASPcoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPcoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly OrganizationContext context;

        public EmployeeAPIController(OrganizationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            var data = await context.Employees.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        { 
            var employee = await context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee emp)
        {
           await context.Employees.AddAsync(emp);
            await context.SaveChangesAsync();
            return Ok(emp);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee emp)
        {
            if(id != emp.EmployeeId)
            {
                return BadRequest();
            }
            context.Entry(emp).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var emp = await context.Employees.FindAsync(id);
            if(emp == null)
            { 
                return NotFound();
            }
            context.Employees.Remove(emp);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}

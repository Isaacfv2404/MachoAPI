using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MachoBateriasAPI.Data;
using MachoBateriasAPI.Models;

namespace MachoBateriasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly MachoBateriasAPIContext _context;

        public EmployeesController(MachoBateriasAPIContext context)
        {
            _context = context;
        }
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetSupplier()
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            return await _context.Employee.ToListAsync();
        }
    }
}

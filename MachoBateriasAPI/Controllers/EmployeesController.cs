﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MachoBateriasAPI.Data;
using MachoBateriasAPI.Models;
using Microsoft.CodeAnalysis.Scripting;

namespace MachoBateriasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly MachoBateriasAPIContext _context;

        public EmployeesController(MachoBateriasAPIContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
          if (_context.Employee == null)
          {
              return NotFound();
          }
            return await _context.Employee.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employee == null)
          {
              return NotFound();
          }
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employee == null)
          {
              return Problem("Entity set 'MachoBateriasAPIContext.Employee'  is null.");
          }
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpPost("login")]
        [HttpPost("login/{id}")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            Console.WriteLine("HOLAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine(loginModel.Email);
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.email == loginModel.Email);

            if (employee == null || !VerifyPassword(employee.password, loginModel.Password))
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            // Guardar la información en la sesión
            HttpContext.Session.SetInt32("id", employee.id);
            HttpContext.Session.SetString("email", employee.email);

            return Ok(new { message = "Inicio de sesión exitoso" });
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        private bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }


    }
}

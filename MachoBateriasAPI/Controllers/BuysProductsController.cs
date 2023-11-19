using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MachoBateriasAPI.Data;
using MachoBateriasAPI.Models;

namespace MachoBateriasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuysProductsController : ControllerBase
    {
        private readonly MachoBateriasAPIContext _context;

        public BuysProductsController(MachoBateriasAPIContext context)
        {
            _context = context;
        }

        // GET: api/BuysProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuysProduct>>> GetBuysProduct()
        {
          if (_context.BuysProduct == null)
          {
              return NotFound();
          }
            return await _context.BuysProduct.ToListAsync();
        }

        // GET: api/BuysProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuysProduct>> GetBuysProduct(int id)
        {
          if (_context.BuysProduct == null)
          {
              return NotFound();
          }
            var buysProduct = await _context.BuysProduct.FindAsync(id);

            if (buysProduct == null)
            {
                return NotFound();
            }

            return buysProduct;
        }

        // PUT: api/BuysProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuysProduct(int id, BuysProduct buysProduct)
        {
            if (id != buysProduct.id)
            {
                return BadRequest();
            }

            _context.Entry(buysProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuysProductExists(id))
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

        // POST: api/BuysProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BuysProduct>> PostBuysProduct(BuysProduct buysProduct)
        {
          if (_context.BuysProduct == null)
          {
              return Problem("Entity set 'MachoBateriasAPIContext.BuysProduct'  is null.");
          }
            _context.BuysProduct.Add(buysProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuysProduct", new { id = buysProduct.id }, buysProduct);
        }

        // DELETE: api/BuysProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuysProduct(int id)
        {
            if (_context.BuysProduct == null)
            {
                return NotFound();
            }
            var buysProduct = await _context.BuysProduct.FindAsync(id);
            if (buysProduct == null)
            {
                return NotFound();
            }

            _context.BuysProduct.Remove(buysProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuysProductExists(int id)
        {
            return (_context.BuysProduct?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET105_ASM.Models;

namespace NET105_ASM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboFoodItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComboFoodItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ComboFoodItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboFoodItem>>> GetComboFoodItems()
        {
            return await _context.ComboFoodItems.ToListAsync();
        }

        // GET: api/ComboFoodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComboFoodItem>> GetComboFoodItem(int id)
        {
            var comboFoodItem = await _context.ComboFoodItems.FindAsync(id);

            if (comboFoodItem == null)
            {
                return NotFound();
            }

            return comboFoodItem;
        }

        // PUT: api/ComboFoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComboFoodItem(int id, ComboFoodItem comboFoodItem)
        {
            if (id != comboFoodItem.ComboId)
            {
                return BadRequest();
            }

            _context.Entry(comboFoodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComboFoodItemExists(id))
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

        // POST: api/ComboFoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComboFoodItem>> PostComboFoodItem(ComboFoodItem comboFoodItem)
        {
            _context.ComboFoodItems.Add(comboFoodItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComboFoodItemExists(comboFoodItem.ComboId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetComboFoodItem", new { id = comboFoodItem.ComboId }, comboFoodItem);
        }

        // DELETE: api/ComboFoodItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComboFoodItem(int id)
        {
            var comboFoodItem = await _context.ComboFoodItems.FindAsync(id);
            if (comboFoodItem == null)
            {
                return NotFound();
            }

            _context.ComboFoodItems.Remove(comboFoodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComboFoodItemExists(int id)
        {
            return _context.ComboFoodItems.Any(e => e.ComboId == id);
        }
    }
}

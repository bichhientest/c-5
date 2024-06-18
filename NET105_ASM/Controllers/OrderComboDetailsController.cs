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
    public class OrderComboDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderComboDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderComboDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderComboDetail>>> GetOrderComboDetails()
        {
            return await _context.OrderComboDetails.ToListAsync();
        }

        // GET: api/OrderComboDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderComboDetail>> GetOrderComboDetail(int id)
        {
            var orderComboDetail = await _context.OrderComboDetails.FindAsync(id);

            if (orderComboDetail == null)
            {
                return NotFound();
            }

            return orderComboDetail;
        }

        // PUT: api/OrderComboDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderComboDetail(int id, OrderComboDetail orderComboDetail)
        {
            if (id != orderComboDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderComboDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderComboDetailExists(id))
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

        // POST: api/OrderComboDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderComboDetail>> PostOrderComboDetail(OrderComboDetail orderComboDetail)
        {
            _context.OrderComboDetails.Add(orderComboDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderComboDetail", new { id = orderComboDetail.Id }, orderComboDetail);
        }

        // DELETE: api/OrderComboDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderComboDetail(int id)
        {
            var orderComboDetail = await _context.OrderComboDetails.FindAsync(id);
            if (orderComboDetail == null)
            {
                return NotFound();
            }

            _context.OrderComboDetails.Remove(orderComboDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderComboDetailExists(int id)
        {
            return _context.OrderComboDetails.Any(e => e.Id == id);
        }
    }
}

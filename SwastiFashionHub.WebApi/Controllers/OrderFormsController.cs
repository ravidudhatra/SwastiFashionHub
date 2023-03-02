using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Data.Context;
using SwastiFashionHub.Data.Models;

namespace SwastiFashionHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderFormsController : ControllerBase
    {
        private readonly SwastiFashionHubLlpContext _context;

        public OrderFormsController(SwastiFashionHubLlpContext context)
        {
            _context = context;
        }

        // GET: api/OrderForms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderForm>>> GetOrderForms()
        {
            if (_context.OrderForms == null)
            {
                return NotFound();
            }
            return await _context.OrderForms.ToListAsync();
        }

        // GET: api/OrderForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderForm>> GetOrderForm(long id)
        {
            if (_context.OrderForms == null)
            {
                return NotFound();
            }
            var orderForm = await _context.OrderForms.FindAsync(id);

            if (orderForm == null)
            {
                return NotFound();
            }

            return orderForm;
        }

        // PUT: api/OrderForms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderForm(long id, OrderForm orderForm)
        {
            if (id != orderForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderFormExists(id))
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

        // POST: api/OrderForms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderForm>> PostOrderForm(OrderForm orderForm)
        {
            if (_context.OrderForms == null)
            {
                return Problem("Entity set 'SwastiFashionHubLlpContext.OrderForms'  is null.");
            }

            _context.OrderForms.Add(orderForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderForm", new { id = orderForm.Id }, orderForm);
        }

        // DELETE: api/OrderForms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderForm(long id)
        {
            if (_context.OrderForms == null)
            {
                return NotFound();
            }
            var orderForm = await _context.OrderForms.FindAsync(id);
            if (orderForm == null)
            {
                return NotFound();
            }

            _context.OrderForms.Remove(orderForm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderFormExists(long id)
        {
            return (_context.OrderForms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

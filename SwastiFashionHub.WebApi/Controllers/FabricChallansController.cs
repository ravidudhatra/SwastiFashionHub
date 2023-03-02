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
    public class FabricChallansController : ControllerBase
    {
        private readonly SwastiFashionHubLlpContext _context;

        public FabricChallansController(SwastiFashionHubLlpContext context)
        {
            _context = context;
        }

        // GET: api/FabricChallans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FabricChallan>>> GetFabricChallans()
        {
          if (_context.FabricChallans == null)
          {
              return NotFound();
          }
            return await _context.FabricChallans.ToListAsync();
        }

        // GET: api/FabricChallans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FabricChallan>> GetFabricChallan(long id)
        {
          if (_context.FabricChallans == null)
          {
              return NotFound();
          }
            var fabricChallan = await _context.FabricChallans.FindAsync(id);

            if (fabricChallan == null)
            {
                return NotFound();
            }

            return fabricChallan;
        }

        // PUT: api/FabricChallans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFabricChallan(long id, FabricChallan fabricChallan)
        {
            if (id != fabricChallan.Id)
            {
                return BadRequest();
            }

            _context.Entry(fabricChallan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricChallanExists(id))
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

        // POST: api/FabricChallans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FabricChallan>> PostFabricChallan(FabricChallan fabricChallan)
        {
          if (_context.FabricChallans == null)
          {
              return Problem("Entity set 'SwastiFashionHubLlpContext.FabricChallans'  is null.");
          }
            _context.FabricChallans.Add(fabricChallan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFabricChallan", new { id = fabricChallan.Id }, fabricChallan);
        }

        // DELETE: api/FabricChallans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabricChallan(long id)
        {
            if (_context.FabricChallans == null)
            {
                return NotFound();
            }
            var fabricChallan = await _context.FabricChallans.FindAsync(id);
            if (fabricChallan == null)
            {
                return NotFound();
            }

            _context.FabricChallans.Remove(fabricChallan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FabricChallanExists(long id)
        {
            return (_context.FabricChallans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

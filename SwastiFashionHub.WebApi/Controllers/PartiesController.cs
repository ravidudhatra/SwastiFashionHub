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
    public class PartiesController : ControllerBase
    {
        private readonly SwastiFashionHubLlpContext _context;

        public PartiesController(SwastiFashionHubLlpContext context)
        {
            _context = context;
        }

        // GET: api/Parties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Party>>> GetParties()
        {
          if (_context.Parties == null)
          {
              return NotFound();
          }
            return await _context.Parties.ToListAsync();
        }

        // GET: api/Parties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Party>> GetParty(int id)
        {
          if (_context.Parties == null)
          {
              return NotFound();
          }
            var party = await _context.Parties.FindAsync(id);

            if (party == null)
            {
                return NotFound();
            }

            return party;
        }

        // PUT: api/Parties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParty(int id, Party party)
        {
            if (id != party.Id)
            {
                return BadRequest();
            }

            _context.Entry(party).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyExists(id))
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

        // POST: api/Parties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Party>> PostParty(Party party)
        {
          if (_context.Parties == null)
          {
              return Problem("Entity set 'SwastiFashionHubLlpContext.Parties'  is null.");
          }
            _context.Parties.Add(party);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParty", new { id = party.Id }, party);
        }

        // DELETE: api/Parties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParty(int id)
        {
            if (_context.Parties == null)
            {
                return NotFound();
            }
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartyExists(int id)
        {
            return (_context.Parties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

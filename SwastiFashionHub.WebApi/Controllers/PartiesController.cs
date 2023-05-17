using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SwastiFashionHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartiesController : ControllerBase
    {

        private readonly IPartyService _partyService;
        private readonly IWebHostEnvironment _environment;
        public PartiesController(IPartyService partyService,
            IWebHostEnvironment environment)
        {
            _partyService = partyService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetParties()
        {
            var result = await _partyService.GetAllAsync();
            return Ok(result);
        }


        // GET: api/Parties
        [HttpGet("ByPagination")]
        public async Task<IActionResult> GetParties([FromQuery] PaginatedRequest paginatedRequest)
        {
            var result = await _partyService.GetAllAsync(
                paginatedRequest.Search ?? string.Empty,
                paginatedRequest.PageNumber,
                paginatedRequest.PageSize,
                paginatedRequest.OrderBy);

            return Ok(result);
        }

        // GET: api/Parties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyResponse>> GetParty(Guid id)
        {
            var result = await _partyService.GetAsync(id);
            return Ok(result);
        }

        // PUT: api/Parties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParty(Guid id, [FromBody] PartyRequest party)
        {
            var result = await _partyService.UpdateAsync(party);
            return Ok(result);
        }

        // POST: api/Parties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostParty([FromBody] PartyRequest party)
        {
            var result = await _partyService.SaveAsync(party);
            return Ok(result);
        }

        // DELETE: api/Parties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParty(Guid id)
        {
            var result = await _partyService.DeleteAsync(id);
            return Ok(result);
        }
    }
}

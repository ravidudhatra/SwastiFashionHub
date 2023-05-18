using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Services.Interface;

namespace SwastiFashionHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricChallansController : ControllerBase
    {
        private readonly IFabricChallanService _fabricChallanService;
        private readonly IWebHostEnvironment _environment;
        public FabricChallansController(IFabricChallanService fabricChallanService,
            IWebHostEnvironment environment)
        {
            _fabricChallanService = fabricChallanService;
            _environment = environment;
        }

        // GET: api/FabricChallanChallans
        [HttpGet]
        public async Task<IActionResult> GetFabricChallanChallans()
        {
            var result = await _fabricChallanService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/FabricChallanChallans
        [HttpGet("ByPagination")]
        public async Task<IActionResult> GetFabricChallanChallans([FromQuery] PaginatedRequest paginatedRequest)
        {
            var result = await _fabricChallanService.GetAllAsync(
                paginatedRequest.Search ?? string.Empty,
                paginatedRequest.PageNumber,
                paginatedRequest.PageSize,
                paginatedRequest.OrderBy);

            return Ok(result);
        }

        // GET: api/FabricChallanChallans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FabricChallanResponse>> GetFabricChallan(Guid id)
        {
            var result = await _fabricChallanService.GetAsync(id);
            return Ok(result);
        }

        // PUT: api/FabricChallanChallans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFabricChallan(Guid id, [FromBody] FabricChallanRequest FabricChallan)
        {
            var result = await _fabricChallanService.UpdateAsync(FabricChallan);
            return Ok(result);
        }

        // POST: api/FabricChallanChallans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostFabricChallan([FromBody] FabricChallanRequest FabricChallan)
        {
            var result = await _fabricChallanService.SaveAsync(FabricChallan);
            return Ok(result);
        }

        // DELETE: api/FabricChallanChallans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabricChallan(Guid id)
        {
            var result = await _fabricChallanService.DeleteAsync(id);
            return Ok(result);
        }
    }
}

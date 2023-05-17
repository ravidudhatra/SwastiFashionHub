using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Services.Interface;

namespace SwastiFashionHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricsController : ControllerBase
    {
        private readonly IFabricService _Fabricservice;
        private readonly IWebHostEnvironment _environment;
        public FabricsController(IFabricService Fabricservice,
            IWebHostEnvironment environment)
        {
            _Fabricservice = Fabricservice;
            _environment = environment;
        }

        // GET: api/Fabrics
        [HttpGet]
        public async Task<IActionResult> GetFabrics()
        {
            var result = await _Fabricservice.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Fabrics
        [HttpGet("ByPagination")]
        public async Task<IActionResult> GetFabrics([FromQuery] PaginatedRequest paginatedRequest)
        {
            var result = await _Fabricservice.GetAllAsync(
                paginatedRequest.Search ?? string.Empty,
                paginatedRequest.PageNumber,
                paginatedRequest.PageSize,
                paginatedRequest.OrderBy);

            return Ok(result);
        }

        // GET: api/Fabrics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FabricResponse>> GetFabric(Guid id)
        {
            var result = await _Fabricservice.GetAsync(id);
            return Ok(result);
        }

        // PUT: api/Fabrics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFabric(Guid id, [FromForm] FabricRequest Fabric)
        {
            var result = await _Fabricservice.UpdateAsync(Fabric);
            return Ok(result);
        }

        // POST: api/Fabrics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostFabric([FromForm] FabricRequest Fabric)
        {
            var result = await _Fabricservice.SaveAsync(Fabric);
            return Ok(result);
        }

        // DELETE: api/Fabrics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabric(Guid id)
        {
            var result = await _Fabricservice.DeleteAsync(id);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Services.Interface;

namespace SwastiFashionHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignsController : ControllerBase
    {
        private readonly IDesignService _designService;
        private static readonly string[] _validExtensions = { "jpg", "gif", "png", "jpeg" };

        public DesignsController(IDesignService designService)
        {
            _designService = designService;
        }

        // GET: api/Designs
        [HttpGet]
        public async Task<IActionResult> GetDesigns()
        {
            var result = await _designService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Designs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DesignResponse>> GetDesign(Guid id)
        {
            var result = await _designService.GetAsync(id);
            return Ok(result);
        }

        // PUT: api/Designs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesign(Guid id, [FromBody] DesignRequest design)
        {
            var result = await _designService.UpdateAsync(design);
            return Ok(result);
        }

        // POST: api/Designs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDesign([FromBody] DesignRequest design)
        {
            var result = await _designService.SaveAsync(design);
            return Ok(result);
        }

        // DELETE: api/Designs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesign(Guid id)
        {
            var result = await _designService.DeleteAsync(id);
            return Ok(result);
        }

        //      /// <summary>
        ///// To upload design images
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //[HttpPost("[action]"), DisableRequestSizeLimit]
        //      public async Task<ActionResult> FileUploadAsync([FromForm] List<IFormFile> file)
        //      {
        //          foreach (var item in file)
        //          {
        //              if (IsImageExtension(item.FileName.Split(".")[1]))
        //              {
        //                  var result = await _blobService.UploadFileBlobAsync(item.FileName, file);
        //                  return Ok(result);
        //              }
        //              else
        //              {
        //                  return BadRequest(Result.Fail("File should be an image."));
        //              }
        //          }
        //      }

        private static bool IsImageExtension(string ext)
        {
            return _validExtensions.Contains(ext.ToLower());
        }
    }
}

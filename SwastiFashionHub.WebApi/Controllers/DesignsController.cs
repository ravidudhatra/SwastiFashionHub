using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core;
using SwastiFashionHub.Core.Services.Interface;

namespace SwastiFashionHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignsController : ControllerBase
    {
        private readonly IDesignService _designService;
        private static readonly string[] _validExtensions = { "jpg", "gif", "png", "jpeg" };
        private readonly IWebHostEnvironment _environment;
        public DesignsController(IDesignService designService,
            IWebHostEnvironment environment)
        {
            _designService = designService;
            _environment = environment;
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
        public async Task<IActionResult> PostDesign([FromForm] DesignRequest design)
        {
            var result = await _designService.SaveAsync(design);

            if (design.Id != Guid.Empty)
            {
                foreach (var item in design.NewImages)
                    await UploadFile(item, design.Id.ToString());
            }

            return Ok(result);
        }

        // DELETE: api/Designs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesign(Guid id)
        {
            var basePath = $"{Constants.ImageStoreBasePath}\\{id}\\";
            var fullPath = Path.Combine(_environment.ContentRootPath + basePath);
            var result = await _designService.DeleteAsync(id, fullPath);
            return Ok(result);
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> UploadDesignAsync(List<IFormFile> files, string currentDirectory)
        //{
        //    try
        //    {
        //        foreach (var file in files)
        //        {
        //            var basePath = Path.Combine(Directory.GetCurrentDirectory() + $"\\uploads\\Design\\{currentDirectory}\\");
        //            bool basePathExists = Directory.Exists(basePath);
        //            if (!basePathExists) Directory.CreateDirectory(basePath);
        //            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        //            var filePath = Path.Combine(basePath, file.FileName);
        //            var extension = Path.GetExtension(file.FileName);
        //            if (!System.IO.File.Exists(filePath))
        //            {
        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(stream);
        //                }

        //                var designImagesRequest = new DesignImagesRequest
        //                {
        //                    DesignId = Guid.Parse(currentDirectory),
        //                    FileType = file.ContentType,
        //                    Extension = extension,
        //                    Name = fileName,
        //                    ImageUrl = filePath
        //                };
        //                await _designService.SaveDesignImageAsync(designImagesRequest);
        //            }
        //        }
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPost("[action]")]
        private async Task UploadFile(IFormFile file, string designId)
        {
            try
            {
                if (file != null)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var buffer = new byte[stream.Length];
                        await stream.ReadAsync(buffer, 0, (int)stream.Length);

                        // Save the file to the file system
                        var basePath = $"{Constants.ImageStoreBasePath}\\{designId}\\";
                        var fullPath = Path.Combine(_environment.ContentRootPath + basePath);
                        bool basePathExists = Directory.Exists(fullPath);
                        if (!basePathExists) Directory.CreateDirectory(fullPath);
                        System.IO.File.WriteAllBytes(fullPath + file.FileName, buffer);

                        // Save the file path to the database
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var filePath = Path.Combine(basePath, file.FileName);
                        var extension = Path.GetExtension(file.FileName);
                        var designImagesRequest = new DesignImagesRequest
                        {
                            DesignId = Guid.Parse(designId),
                            FileType = file.ContentType,
                            Extension = extension,
                            Name = fileName,
                            ImageUrl = filePath // Store the file path in the database
                        };

                        // Save the file model to the database
                        await _designService.SaveDesignImageAsync(designImagesRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpGet("{id}/images")]
        //public async Task<ActionResult<IEnumerable<DesignImageResponse>>> GetImages(int id)
        //{
        //    var design = await _context.Designs.FindAsync(id);

        //    if (design == null)
        //    {
        //        return NotFound();
        //    }

        //    var designImages = await _context.DesignImages
        //        .Where(di => di.DesignId == id)
        //        .Select(di => new DesignImageDto
        //        {
        //            Id = di.Id,
        //            DesignId = di.DesignId,
        //            ImagePath = di.ImagePath,
        //            FileName = di.FileName
        //        })
        //        .ToListAsync();

        //    return Ok(designImages);
        //}
    }
}
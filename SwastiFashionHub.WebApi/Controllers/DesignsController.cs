using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core;
using SwastiFashionHub.Core.Services.Interface;
using System.Security.Policy;

namespace SwastiFashionHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignsController : ControllerBase
    {
        private readonly IDesignService _designService;
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
            var result = await _designService.GetAllAsync(string.Empty);
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
            var basePath = $"{Constants.ImageStoreBasePath}";
            var fullPath = Path.Combine(_environment.WebRootPath + basePath);
            var result = await _designService.DeleteAsync(id, fullPath);
            return Ok(result);
        }


        // DELETE: api/Designs/5
        [HttpDelete("{id}/images")]
        public async Task<IActionResult> DeleteDesignImage(Guid id)
        {
            var result = await _designService.DeleteDesignImageAsync(id, _environment.WebRootPath);
            return Ok(result);
        }

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

                        var extension = Path.GetExtension(file.FileName);

                        // Save the file to the file system
                        string newImageName = string.Format("{0}.{1}", Guid.NewGuid().ToString(), extension);
                        var basePath = $"{Constants.ImageStoreBasePath}\\{designId}\\";
                        var fullPath = Path.Combine(_environment.WebRootPath + basePath);
                        bool basePathExists = Directory.Exists(fullPath);
                        if (!basePathExists) Directory.CreateDirectory(fullPath);
                        System.IO.File.WriteAllBytes(fullPath + newImageName, buffer);

                        // Save the file path to the database
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var filePath = Path.Combine(basePath, newImageName);

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
    }
}
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Core.Services;
using SwastiFashionHub.Core.Services.Interface;
using SwastiFashionHub.Core.Wrapper;
using SwastiFashionHub.Data.Context;
using SwastiFashionHub.Data.Models;

namespace SwastiFashionHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignsController : ControllerBase
    {
        private readonly IDesignService _designService;

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
        public async Task<ActionResult<Design>> GetDesign(Guid id)
        {
            var result = await _designService.GetAsync(id);
            return Ok(result);
        }

        // PUT: api/Designs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesign(Guid id, Design design)
        {
            var result = await _designService.UpdateAsync(design);
            return Ok(result);
        }

        // POST: api/Designs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDesign(Design design)
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
    }
}

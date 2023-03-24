using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Core.Services.Interface;
using SwastiFashionHub.Core.Wrapper;
using SwastiFashionHub.Data.Context;
using SwastiFashionHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Services
{
    public class DesignService : IDesignService
    {
        protected readonly SwastiFashionHubLlpContext _context;
        public DesignService(SwastiFashionHubLlpContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result<Guid>> SaveAsync(Design design)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                design.CreatedDate = DateTime.Now;
                design.CreatedBy = 0; //todo: get logged in user id and set it.

                _context.Designs.Add(design);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(design.Id, "Design saved successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> UpdateAsync(Design design)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                design.UpdatedDate = DateTime.Now;
                design.UpdatedBy = 0; //todo: get logged in user id and set it.

                _context.Entry(design).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(design.Id, "Design updated successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Design>> GetAsync(Guid id)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Design>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.Designs.FindAsync(id);
                if (data == null)
                    return await Result<Design>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                return await Result<Design>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<Design>.FailAsync("Failed");
            }
        }

        public async Task<Result<List<Design>>> GetAllAsync(string search = "")
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<List<Design>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var queryable = _context.Designs.AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(search.ToLower())).Distinct();

                var designQuery = await queryable.AsNoTracking().ToListAsync();
                if (designQuery == null)
                    return await Result<List<Design>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                return await Result<List<Design>>.SuccessAsync(designQuery);

            }
            catch (Exception ex)
            {
                return await Result<List<Design>>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> DeleteAsync(Guid id)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.Designs.FindAsync(id);
                if (data == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                _context.Designs.Remove(data);
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(id, "Design deleted successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }
    }
}

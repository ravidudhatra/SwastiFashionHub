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


        public async Task<Result<Design>> GetDesignAsync(int id)
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

        public async Task<Result<List<Design>>> GetAllDesignAsync(string search = "")
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
    }
}

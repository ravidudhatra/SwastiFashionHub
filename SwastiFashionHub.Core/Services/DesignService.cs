using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Services.Interface;
using SwastiFashionHub.Core.Wrapper;
using SwastiFashionHub.Data.Context;
using SwastiFashionHub.Data.Models;
using System.Net;

namespace SwastiFashionHub.Core.Services
{
    public class DesignService : IDesignService
    {
        protected readonly SwastiFashionHubLlpContext _context;
        private readonly IMapper _mapper;
        public DesignService(SwastiFashionHubLlpContext dbContext,
            IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> SaveAsync(DesignRequest design)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var objModel = _mapper.Map<Design>(design);
                //var objModel = new Design();
                //objModel.Id = design.Id;
                //objModel.Name = design.Name;
                //objModel.Note = design.Note;
                objModel.CreatedDate = DateTime.Now;
                objModel.CreatedBy = 0; //todo: get logged in user id and set it.

                _context.Designs.Add(objModel);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(design.Id, "Design saved successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> UpdateAsync(DesignRequest design)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var objModel = await _context.Designs.FindAsync(design.Id);

                if (objModel == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                objModel.UpdatedDate = DateTime.Now;
                objModel.UpdatedBy = 0; //todo: get logged in user id and set it.
                objModel.Name = design.Name;
                objModel.Note = design.Note;

                _context.Entry(objModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(design.Id, "Design updated successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<DesignResponse>> GetAsync(Guid id)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<DesignResponse>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var query = await _context.Designs.FindAsync(id);
                if (query == null)
                    return await Result<DesignResponse>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = _mapper.Map<DesignResponse>(query);
                return await Result<DesignResponse>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<DesignResponse>.FailAsync("Failed");
            }
        }

        public async Task<Result<List<DesignResponse>>> GetAllAsync(string search = "")
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<List<DesignResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var queryable = _context.Designs.AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(search.ToLower())).Distinct();

                var query = await queryable.AsNoTracking().ToListAsync();
                if (query == null)
                    return await Result<List<DesignResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = _mapper.Map<List<DesignResponse>>(query);
                return await Result<List<DesignResponse>>.SuccessAsync(data);

            }
            catch (Exception ex)
            {
                return await Result<List<DesignResponse>>.FailAsync("Failed");
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

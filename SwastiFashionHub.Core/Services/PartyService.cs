using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Services.Interface;
using SwastiFashionHub.Core.Wrapper;
using SwastiFashionHub.Data.Context;
using SwastiFashionHub.Data.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Linq.Dynamic.Core;
using Radzen;
using SwastiFashionHub.Core.Extensions;
using Microsoft.Data.SqlClient;
using SwastiFashionHub.Core.Exceptions;
using Azure.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SwastiFashionHub.Core.Services
{
    public class PartyService : IPartyService
    {
        protected readonly SwastiFashionHubLlpContext _context;
        private readonly IMapper _mapper;
        public PartyService(SwastiFashionHubLlpContext dbContext,
            IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<PartyResponse>> GetAsync(Guid id)
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<PartyResponse>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var query = await _context.Parties.FindAsync(id);
                if (query == null)
                    return await Result<PartyResponse>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = _mapper.Map<PartyResponse>(query);
                return await Result<PartyResponse>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<PartyResponse>.FailAsync("Failed");
            }
        }

        public async Task<Result<List<PartyResponse>>> GetAllAsync()
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<List<PartyResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var queryable = (from item in _context.Parties.Where(x => x.IsArchived == false)
                                 select new PartyResponse
                                 {
                                     Id = item.Id,
                                     CreatedBy = item.CreatedBy,
                                     CreatedDate = item.CreatedDate,
                                     Name = item.Name,
                                     PartyType = item.PartyType,
                                     UpdatedBy = item.UpdatedBy
                                 }).AsQueryable();

                if (queryable == null)
                    return await Result<List<PartyResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = _mapper.Map<List<PartyResponse>>(queryable);
                return await Result<List<PartyResponse>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<PartyResponse>>.FailAsync("Failed");
            }
        }

        public async Task<PaginatedResult<PartyResponse>> GetAllAsync(string search, int pageNumber, int pageSize, string? orderBy)
        {
            try
            {
                if (_context.Parties == null)
                {
                    var exception = new CustomException("Not Found!", HttpStatusCode.NotFound);
                    throw exception;
                }

                var queryable = (from item in _context.Parties.Where(x => x.IsArchived == false)
                                 select new PartyResponse
                                 {
                                     Id = item.Id,
                                     CreatedBy = item.CreatedBy,
                                     CreatedDate = item.CreatedDate,
                                     Name = item.Name,
                                     PartyType = item.PartyType,
                                     UpdatedBy = item.UpdatedBy
                                 }).AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(search.ToLower())).Distinct();

                if (queryable == null || queryable.Count() <= 0)
                {
                    PaginatedResult<PartyResponse> emptyQueryResponse = new PaginatedResult<PartyResponse>(null);
                    return emptyQueryResponse;
                }

                var response = await queryable.AsNoTracking().ToPaginatedListAsync(pageNumber, pageSize, orderBy);

                PaginatedResult<PartyResponse> responseQuery = new PaginatedResult<PartyResponse>(response.Data);
                responseQuery.TotalCount = response.TotalCount;
                responseQuery.CurrentPage = response.CurrentPage;
                responseQuery.TotalPages = response.TotalPages;
                responseQuery.PageSize = response.PageSize;
                return responseQuery;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Result<Guid>> SaveAsync(PartyRequest addRequestObject)
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var isExists = _context.Parties.Any(x => x.Name == addRequestObject.Name);
                if (isExists)
                    return await Result<Guid>.FailAsync("Party already exists");

                var objModel = _mapper.Map<Party>(addRequestObject);
                objModel.CreatedDate = DateTime.Now;
                objModel.CreatedBy = 0; //todo: get logged in user id and set it.

                _context.Parties.Add(objModel);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(addRequestObject.Id, "Party saved successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> UpdateAsync(PartyRequest updateRequestObject)
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var isExists = _context.Parties.Any(x => x.Name == updateRequestObject.Name && x.Id != updateRequestObject.Id);
                if (isExists)
                    return await Result<Guid>.FailAsync("Party already exists");

                var objModel = await _context.Parties.FindAsync(updateRequestObject.Id);

                if (objModel == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                objModel.UpdatedDate = DateTime.Now;
                objModel.UpdatedBy = 0; //todo: get logged in user id and set it.
                objModel.Name = updateRequestObject.Name;
                objModel.PartyType = updateRequestObject.PartyType;

                _context.Entry(objModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(updateRequestObject.Id, "Party updated successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> DeleteAsync(Guid id)
        {
            try
            {
                if (_context.Parties == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.Parties.FindAsync(id);
                if (data == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                _context.Parties.Remove(data);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(id, "Party deleted successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }
    }
}

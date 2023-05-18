using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Exceptions;
using SwastiFashionHub.Core.Extensions;
using SwastiFashionHub.Core.Services.Interface;
using SwastiFashionHub.Core.Wrapper;
using SwastiFashionHub.Data.Context;
using SwastiFashionHub.Data.Models;
using System;
using System.Net;
using System.Runtime;

namespace SwastiFashionHub.Core.Services
{
    public class FabricChallanService : IFabricChallanService
    {
        protected readonly SwastiFashionHubLlpContext _context;
        private readonly IMapper _mapper;
        public FabricChallanService(SwastiFashionHubLlpContext dbContext,
            IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> SaveAsync(FabricChallanRequest addRequestObject)
        {
            try
            {
                if (_context.FabricChallans == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var isExists = _context.FabricChallans.Any(x => x.ChallanNo == addRequestObject.ChallanNo);
                if (isExists)
                    return await Result<Guid>.FailAsync("FabricChallan already exists");

                var objModel = _mapper.Map<FabricChallan>(addRequestObject);
                objModel.CreatedDate = DateTime.Now;
                objModel.CreatedBy = 0; //todo: get logged in user id and set it.

                _context.FabricChallans.Add(objModel);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(addRequestObject.Id, "FabricChallan saved successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> UpdateAsync(FabricChallanRequest updateRequestObject)
        {
            try
            {
                if (_context.FabricChallans == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var objModel = await _context.FabricChallans.FindAsync(updateRequestObject.Id);

                if (objModel == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                objModel.UpdatedDate = DateTime.Now;
                objModel.UpdatedBy = 0; //todo: get logged in user id and set it.
                objModel.ChallanDate = updateRequestObject.ChallanDate;
                objModel.ChallanNo = updateRequestObject.ChallanNo;
                objModel.FabricId = updateRequestObject.FabricId;
                objModel.FabricType = updateRequestObject.FabricType;
                objModel.PartyId = updateRequestObject.PartyId;
                objModel.TakaDetail = updateRequestObject.TakaDetail;
                objModel.ChallanImage = updateRequestObject.ChallanImage;
                _context.Entry(objModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(updateRequestObject.Id, "FabricChallan updated successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<FabricChallanResponse>> GetAsync(Guid id)
        {
            try
            {
                if (_context.FabricChallans == null)
                    return await Result<FabricChallanResponse>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var query = await _context.FabricChallans.FindAsync(id);
                if (query == null)
                    return await Result<FabricChallanResponse>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = _mapper.Map<FabricChallanResponse>(query);
                return await Result<FabricChallanResponse>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<FabricChallanResponse>.FailAsync("Failed");
            }
        }

        public async Task<Result<List<FabricChallanResponse>>> GetAllAsync()
        {
            try
            {
                if (_context.FabricChallans == null)
                    return await Result<List<FabricChallanResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var queryable = (from FabricChallan in _context.FabricChallans.Where(x => x.IsArchived == false)
                                 select new FabricChallanResponse
                                 {
                                     Id = FabricChallan.Id,
                                     CreatedBy = FabricChallan.CreatedBy,
                                     CreatedDate = FabricChallan.CreatedDate,
                                     ChallanDate = FabricChallan.ChallanDate,
                                     ChallanImage = FabricChallan.ChallanImage,
                                     ChallanNo = FabricChallan.ChallanNo,
                                     FabricId = FabricChallan.FabricId,
                                     FabricType = FabricChallan.FabricType,
                                     PartyId = FabricChallan.PartyId,
                                     TakaDetail = FabricChallan.TakaDetail,
                                     UpdatedBy = FabricChallan.UpdatedBy
                                 }).AsQueryable();

                var query = await queryable.AsNoTracking().ToListAsync();

                if (query == null)
                    return await Result<List<FabricChallanResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                return await Result<List<FabricChallanResponse>>.SuccessAsync(query);

            }
            catch (Exception ex)
            {
                return await Result<List<FabricChallanResponse>>.FailAsync("Failed");
            }
        }
        public async Task<PaginatedResult<FabricChallanResponse>> GetAllAsync(string search, int pageNumber, int pageSize, string? orderBy)
        {
            try
            {
                if (_context.Parties == null)
                {
                    var exception = new CustomException("Not Found!", HttpStatusCode.NotFound);
                    throw exception;
                }

                var queryable = (from FabricChallan in _context.FabricChallans.Where(x => x.IsArchived == false)
                                 select new FabricChallanResponse
                                 {
                                     Id = FabricChallan.Id,
                                     CreatedBy = FabricChallan.CreatedBy,
                                     CreatedDate = FabricChallan.CreatedDate,
                                     ChallanDate = FabricChallan.ChallanDate,
                                     ChallanImage = FabricChallan.ChallanImage,
                                     ChallanNo = FabricChallan.ChallanNo,
                                     FabricId = FabricChallan.FabricId,
                                     FabricType = FabricChallan.FabricType,
                                     PartyId = FabricChallan.PartyId,
                                     TakaDetail = FabricChallan.TakaDetail,
                                     UpdatedBy = FabricChallan.UpdatedBy
                                 }).AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                    queryable = queryable.Where(x => x.ChallanNo.ToLower().Contains(search.ToLower())).Distinct();

                if (queryable == null || queryable.Count() <= 0)
                {
                    PaginatedResult<FabricChallanResponse> emptyQueryResponse = new PaginatedResult<FabricChallanResponse>(null);
                    return emptyQueryResponse;
                }

                var response = await queryable.AsNoTracking().ToPaginatedListAsync(pageNumber, pageSize, orderBy);

                PaginatedResult<FabricChallanResponse> responseQuery = new PaginatedResult<FabricChallanResponse>(response.Data);
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

        public async Task<Result<Guid>> DeleteAsync(Guid id)
        {
            try
            {
                if (_context.FabricChallans == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.FabricChallans.FindAsync(id);
                if (data == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                _context.FabricChallans.Remove(data);
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(id, "FabricChallan deleted successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }
    }
}
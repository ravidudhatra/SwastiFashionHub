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

namespace SwastiFashionHub.Core.Services
{
    public class FabricService : IFabricService
    {
        protected readonly SwastiFashionHubLlpContext _context;
        private readonly IMapper _mapper;
        public FabricService(SwastiFashionHubLlpContext dbContext,
            IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> SaveAsync(FabricRequest addRequestObject)
        {
            try
            {
                if (_context.Fabrics == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var isExists = _context.Fabrics.Any(x => x.Name == addRequestObject.Name);
                if (isExists)
                    return await Result<Guid>.FailAsync("Fabric already exists");

                var objModel = _mapper.Map<Fabric>(addRequestObject);
                objModel.CreatedDate = DateTime.Now;
                objModel.CreatedBy = 0; //todo: get logged in user id and set it.

                _context.Fabrics.Add(objModel);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(addRequestObject.Id, "Fabric saved successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> UpdateAsync(FabricRequest updateRequestObject)
        {
            try
            {
                if (_context.Fabrics == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var objModel = await _context.Fabrics.FindAsync(updateRequestObject.Id);

                if (objModel == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                objModel.UpdatedDate = DateTime.Now;
                objModel.UpdatedBy = 0; //todo: get logged in user id and set it.
                objModel.Name = updateRequestObject.Name;

                _context.Entry(objModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(updateRequestObject.Id, "Fabric updated successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<FabricResponse>> GetAsync(Guid id)
        {
            try
            {
                if (_context.Fabrics == null)
                    return await Result<FabricResponse>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var query = await _context.Fabrics.FindAsync(id);
                if (query == null)
                    return await Result<FabricResponse>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = _mapper.Map<FabricResponse>(query);
                return await Result<FabricResponse>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<FabricResponse>.FailAsync("Failed");
            }
        }

        public async Task<Result<List<FabricResponse>>> GetAllAsync()
        {
            try
            {
                if (_context.Fabrics == null)
                    return await Result<List<FabricResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var queryable = (from Fabric in _context.Fabrics.Where(x => x.IsArchived == false)
                                 select new FabricResponse
                                 {
                                     Id = Fabric.Id,
                                     CreatedBy = Fabric.CreatedBy,
                                     CreatedDate = Fabric.CreatedDate,
                                     Name = Fabric.Name,
                                     UpdatedBy = Fabric.UpdatedBy
                                 }).AsQueryable();

                var query = await queryable.AsNoTracking().ToListAsync();

                if (query == null)
                    return await Result<List<FabricResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                return await Result<List<FabricResponse>>.SuccessAsync(query);

            }
            catch (Exception ex)
            {
                return await Result<List<FabricResponse>>.FailAsync("Failed");
            }
        }
        public async Task<PaginatedResult<FabricResponse>> GetAllAsync(string search, int pageNumber, int pageSize, string? orderBy)
        {
            try
            {
                if (_context.Parties == null)
                {
                    var exception = new CustomException("Not Found!", HttpStatusCode.NotFound);
                    throw exception;
                }

                var queryable = (from Fabric in _context.Fabrics.Where(x => x.IsArchived == false)
                                 select new FabricResponse
                                 {
                                     Id = Fabric.Id,
                                     CreatedBy = Fabric.CreatedBy,
                                     CreatedDate = Fabric.CreatedDate,
                                     Name = Fabric.Name,
                                     UpdatedBy = Fabric.UpdatedBy
                                 }).AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(search.ToLower())).Distinct();

                if (queryable == null || queryable.Count() <= 0)
                {
                    PaginatedResult<FabricResponse> emptyQueryResponse = new PaginatedResult<FabricResponse>(null);
                    return emptyQueryResponse;
                }

                var response = await queryable.AsNoTracking().ToPaginatedListAsync(pageNumber, pageSize, orderBy);

                PaginatedResult<FabricResponse> responseQuery = new PaginatedResult<FabricResponse>(response.Data);
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
                if (_context.Fabrics == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.Fabrics.FindAsync(id);
                if (data == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                //var FabricImagesData = await _context.FabricImages.Where(x => x.FabricId == id).ToListAsync();

                //_context.FabricImages.RemoveRange(FabricImagesData);
                //_context.Fabrics.Remove(data);

                ////remove images from server
                //string directoryPath = string.Format("{0}/{1}", imagePath, id.ToString());
                //bool basePathExists = Directory.Exists(directoryPath);
                //if (basePathExists)
                //    Directory.Delete(directoryPath, true);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(id, "Fabric deleted successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }
    }
}
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
using System.ComponentModel.Design;
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

        public async Task<Result<Guid>> SaveAsync(DesignRequest addRequestObject)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var isExists = _context.Designs.Any(x => x.Name == addRequestObject.Name);
                if (isExists)
                    return await Result<Guid>.FailAsync("Design already exists");

                var objModel = _mapper.Map<Design>(addRequestObject);
                objModel.CreatedDate = DateTime.Now;
                objModel.CreatedBy = 0; //todo: get logged in user id and set it.

                _context.Designs.Add(objModel);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(addRequestObject.Id, "Design saved successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> UpdateAsync(DesignRequest updateRequestObject)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var objModel = await _context.Designs.FindAsync(updateRequestObject.Id);

                if (objModel == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                objModel.UpdatedDate = DateTime.Now;
                objModel.UpdatedBy = 0; //todo: get logged in user id and set it.
                objModel.Name = updateRequestObject.Name;
                objModel.Note = updateRequestObject.Note;

                _context.Entry(objModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(updateRequestObject.Id, "Design updated successfully");
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

        public async Task<Result<List<DesignResponse>>> GetAllAsync()
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<List<DesignResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var queryable = (from design in _context.Designs.Where(x => x.IsArchived == false)
                                 select new DesignResponse
                                 {
                                     Id = design.Id,
                                     CreatedBy = design.CreatedBy,
                                     CreatedDate = design.CreatedDate,
                                     Name = design.Name,
                                     Note = design.Note,
                                     UpdatedBy = design.UpdatedBy,
                                     DesignImages = (from designImages in _context.DesignImages.Where(x => x.DesignId == design.Id)
                                                     select new DesignImageResponse
                                                     {
                                                         Id = designImages.Id,
                                                         DesignId = design.Id,
                                                         ImageUrl = designImages.ImageUrl
                                                     }).ToList()
                                 }).AsQueryable();

                var query = await queryable.AsNoTracking().ToListAsync();
                
                if (query == null)
                    return await Result<List<DesignResponse>>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                return await Result<List<DesignResponse>>.SuccessAsync(query);

            }
            catch (Exception ex)
            {
                return await Result<List<DesignResponse>>.FailAsync("Failed");
            }
        }
        public async Task<PaginatedResult<DesignResponse>> GetAllAsync(string search, int pageNumber, int pageSize, string? orderBy)
        {
            try
            {
                if (_context.Parties == null)
                {
                    var exception = new CustomException("Not Found!", HttpStatusCode.NotFound);
                    throw exception;
                }

                var queryable = (from design in _context.Designs.Where(x => x.IsArchived == false)
                                 select new DesignResponse
                                 {
                                     Id = design.Id,
                                     CreatedBy = design.CreatedBy,
                                     CreatedDate = design.CreatedDate,
                                     Name = design.Name,
                                     Note = design.Note,
                                     UpdatedBy = design.UpdatedBy,
                                     DesignImages = (from designImages in _context.DesignImages.Where(x => x.DesignId == design.Id)
                                                     select new DesignImageResponse
                                                     {
                                                         Id = designImages.Id,
                                                         DesignId = design.Id,
                                                         ImageUrl = designImages.ImageUrl
                                                     }).ToList()
                                 }).AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(search.ToLower())).Distinct();

                if (queryable == null || queryable.Count() <= 0)
                {
                    PaginatedResult<DesignResponse> emptyQueryResponse = new PaginatedResult<DesignResponse>(null);
                    return emptyQueryResponse;
                }

                var response = await queryable.AsNoTracking().ToPaginatedListAsync(pageNumber, pageSize, orderBy);

                PaginatedResult<DesignResponse> responseQuery = new PaginatedResult<DesignResponse>(response.Data);
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

        public async Task<Result<Guid>> DeleteAsync(Guid id, string imagePath)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.Designs.FindAsync(id);
                if (data == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var designImagesData = await _context.DesignImages.Where(x => x.DesignId == id).ToListAsync();

                _context.DesignImages.RemoveRange(designImagesData);
                _context.Designs.Remove(data);

                //remove images from server
                string directoryPath = string.Format("{0}/{1}", imagePath, id.ToString());
                bool basePathExists = Directory.Exists(directoryPath);
                if (basePathExists)
                    Directory.Delete(directoryPath, true);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(id, "Design deleted successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }



        public async Task<Result<Guid>> SaveDesignImageAsync(DesignImagesRequest designImage)
        {
            try
            {
                if (_context.Designs == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var objModel = _mapper.Map<DesignImage>(designImage);

                _context.DesignImages.Add(objModel);

                await _context.SaveChangesAsync();

                return await Result<Guid>.SuccessAsync(designImage.Id, "Design image saved successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

        public async Task<Result<Guid>> DeleteDesignImageAsync(Guid id, string webRootPath)
        {
            try
            {
                if (_context.DesignImages == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                var data = await _context.DesignImages.FindAsync(id);
                if (data == null)
                    return await Result<Guid>.ReturnErrorAsync("Not Found", (int)HttpStatusCode.NotFound);

                _context.DesignImages.Remove(data);

                await _context.SaveChangesAsync();

                //remove images from server
                string filePath = string.Format("{0}/{1}", webRootPath, data.ImageUrl);
                bool basePathExists = File.Exists(filePath);
                if (basePathExists)
                    File.Delete(filePath);

                return await Result<Guid>.SuccessAsync(id, "Design deleted successfully");
            }
            catch (Exception ex)
            {
                return await Result<Guid>.FailAsync("Failed");
            }
        }

    }
}

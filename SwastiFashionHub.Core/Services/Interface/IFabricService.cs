using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Wrapper;
using SwastiFashionHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Services.Interface
{
    public interface IFabricService
    {
        Task<Result<FabricResponse>> GetAsync(Guid id);
        Task<Result<List<FabricResponse>>> GetAllAsync();
        Task<PaginatedResult<FabricResponse>> GetAllAsync(string search, int pageNumber, int pageSize, string? orderBy);
        Task<Result<Guid>> SaveAsync(FabricRequest addRequestObject);
        Task<Result<Guid>> UpdateAsync(FabricRequest updateRequestObject);
        Task<Result<Guid>> DeleteAsync(Guid id);
    }
}

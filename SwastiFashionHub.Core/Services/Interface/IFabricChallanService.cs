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
    public interface IFabricChallanService
    {
        Task<Result<FabricChallanResponse>> GetAsync(Guid id);
        Task<Result<List<FabricChallanResponse>>> GetAllAsync();
        Task<PaginatedResult<FabricChallanResponse>> GetAllAsync(string search, int pageNumber, int pageSize, string? orderBy);
        Task<Result<Guid>> SaveAsync(FabricChallanRequest addRequestObject);
        Task<Result<Guid>> UpdateAsync(FabricChallanRequest updateRequestObject);
        Task<Result<Guid>> DeleteAsync(Guid id);
    }
}

using Radzen;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Core.Wrapper;

namespace SwastiFashionHub.Core.Services.Interface
{
    public interface IPartyService
    {
        Task<Result<PartyResponse>> GetAsync(Guid id);
        Task<Result<List<PartyResponse>>> GetAllAsync();
        Task<PaginatedResult<PartyResponse>> GetAllAsync(string search, int pageNumber, int pageSize, string? orderBy);
        Task<Result<Guid>> SaveAsync(PartyRequest addRequestObject);
        Task<Result<Guid>> UpdateAsync(PartyRequest updateRequestObject);
        Task<Result<Guid>> DeleteAsync(Guid id);
    }
}

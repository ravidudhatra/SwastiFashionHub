using Radzen;
using SwastiFashionHub.Common.Data.Request;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Services.Interface
{
    public interface IPartyService
    {
        Task<PartyResponse> Get(Guid id);
        Task<List<PartyResponse>> GetAll(string search, int pageNumber, int pageSize, string? orderBy);
        Task<List<PartyResponse>> GetAll();
        Task<object> Add(PartyRequest addRequestObject);
        Task<object> Update(PartyRequest updateRequestObject);
        Task Delete(Guid id);
    }
}

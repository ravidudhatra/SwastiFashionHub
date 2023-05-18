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
    public interface IFabricService
    {
        Task<FabricResponse> Get(Guid id);
        Task<List<FabricResponse>> GetAll(string search, int pageNumber, int pageSize, string? orderBy);
        Task<List<FabricResponse>> GetAll();
        Task<object> Add(FabricRequest addRequestObject);
        Task<object> Update(FabricRequest updateRequestObject);
        Task Delete(Guid id);
    }
}

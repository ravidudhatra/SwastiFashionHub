using SwastiFashionHub.Shared.Core.Http;
using SwastiFashionHub.Shared.Core.Services.Interface;
using SwastiFashionHub.Shared.Core.Exceptions;
using SwastiFashionHub.Data.Models;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Common.Data.Request;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Radzen;
using Newtonsoft.Json;

namespace SwastiFashionHub.Shared.Core.Services
{
    public class FabricService : IFabricService
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = "api/fabrics";
        public FabricService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<FabricResponse> Get(Guid id)
        {
            var httpResponse = await httpService.Get<FabricResponse>($"{baseURL}/{id}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<List<FabricResponse>> GetAll()
        {
            var httpResponse = await httpService.Get<List<FabricResponse>>($"{baseURL}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<List<FabricResponse>> GetAll(string search, int pageNumber, int pageSize, string? orderBy)
        {
            PaginatedRequest paginatedRequest = new PaginatedRequest();
            paginatedRequest.OrderDir = orderBy;
            paginatedRequest.PageNumber = pageNumber;
            paginatedRequest.PageSize = pageSize;
            paginatedRequest.Search = search;

            string paginatedRequestJson = JsonConvert.SerializeObject(paginatedRequest);

            var httpResponse = await httpService.Get<List<FabricResponse>>($"{baseURL}?paginatedRequest={paginatedRequestJson}");
            //var httpResponse = await httpService.Get<List<FabricResponse>>($"{baseURL}?Filter={filter}&Skip={skip}&Take={take}&OrderBy={orderBy}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<object> Add(FabricRequest Fabric)
        {
            var httpResponse = await httpService.Post<FabricRequest, object>($"{baseURL}", Fabric);
            var result = await httpResponse.GetResult();
            if (!httpResponse.Success || !result.IsSucceeded)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            return result.Data;
        }

        public async Task<object> Update(FabricRequest updateFabric)
        {
            var httpResponse = await httpService.Put<FabricRequest, object>($"{baseURL}/{updateFabric.Id}", updateFabric);
            var result = await httpResponse.GetResult();
            if (!httpResponse.Success || !result.IsSucceeded)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            return result.Data;
        }

        public async Task Delete(Guid id)
        {
            var httpResponse = await httpService.Delete($"{baseURL}/{id}");
            var result = await httpResponse.GetResult();
            if (!httpResponse.Success || !result.IsSucceeded)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
        }
    }
}

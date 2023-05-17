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
    public class PartyService : IPartyService
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = "api/Parties";
        public PartyService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<PartyResponse> Get(Guid id)
        {
            var httpResponse = await httpService.Get<PartyResponse>($"{baseURL}/{id}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<List<PartyResponse>> GetAll()
        {
            var httpResponse = await httpService.Get<List<PartyResponse>>($"{baseURL}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<List<PartyResponse>> GetAll(string search, int pageNumber, int pageSize, string? orderBy)
        {
            PaginatedRequest paginatedRequest = new PaginatedRequest();
            paginatedRequest.OrderDir = orderBy;
            paginatedRequest.PageNumber = pageNumber;
            paginatedRequest.PageSize = pageSize;
            paginatedRequest.Search = search;

            string paginatedRequestJson = JsonConvert.SerializeObject(paginatedRequest);

            var httpResponse = await httpService.Get<List<PartyResponse>>($"{baseURL}?paginatedRequest={paginatedRequestJson}");
            //var httpResponse = await httpService.Get<List<PartyResponse>>($"{baseURL}?Filter={filter}&Skip={skip}&Take={take}&OrderBy={orderBy}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<object> Add(PartyRequest party)
        {
            var httpResponse = await httpService.Post<PartyRequest, object>($"{baseURL}", party);
            var result = await httpResponse.GetResult();
            if (!httpResponse.Success || !result.IsSucceeded)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            return result.Data;
        }

        public async Task<object> Update(PartyRequest updateParty)
        {
            var httpResponse = await httpService.Put<PartyRequest, object>($"{baseURL}/{updateParty.Id}", updateParty);
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

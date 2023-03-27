using SwastiFashionHub.Shared.Core.Http;
using SwastiFashionHub.Shared.Core.Services.Interface;
using SwastiFashionHub.Shared.Core.Exceptions;
using SwastiFashionHub.Data.Models;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Common.Data.Request;

namespace SwastiFashionHub.Shared.Core.Services
{
    public class DesignService : IDesignService
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = "api/Designs";
        public DesignService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<DesignResponse> Get(Guid id)
        {
            var httpResponse = await httpService.Get<DesignResponse>($"{baseURL}/{id}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<List<DesignResponse>> GetAll()
        {
            var httpResponse = await httpService.Get<List<DesignResponse>>($"{baseURL}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<object> Add(DesignRequest design)
        {
            var httpResponse = await httpService.Post<DesignRequest, object>($"{baseURL}", design);
            var result = await httpResponse.GetResult();
            if (!httpResponse.Success || !result.IsSucceeded)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            return result.Data;
        }

        public async Task<object> Update(DesignRequest updatedesign)
        {
            var httpResponse = await httpService.Put<DesignRequest, object>($"{baseURL}/{updatedesign.Id}", updatedesign);
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

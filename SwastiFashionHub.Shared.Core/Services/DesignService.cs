﻿using SwastiFashionHub.Shared.Core.Http;
using SwastiFashionHub.Shared.Core.Services.Interface;
using SwastiFashionHub.Shared.Core.Exceptions;
using SwastiFashionHub.Data.Models;

namespace SwastiFashionHub.Core.Services
{
    public class DesignService : IDesignService
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = "api/Designs";
        public DesignService(HttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<List<Design>> GetAll()
        {
            var httpResponse = await httpService.Get<List<Design>>($"{baseURL}");
            if (!httpResponse.Success)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            var result = await httpResponse.GetResult();
            return result.Data;
        }

        public async Task<bool> Post(Design design)
        {
            var httpResponse = await httpService.Post<Design, bool>($"{baseURL}", design);
            var result = await httpResponse.GetResult();
            if (!httpResponse.Success || !result.IsSucceeded)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            return result.Data;
        }

        public async Task<bool> Put(Design updatedesign)
        {
            var httpResponse = await httpService.Put<Design, bool>($"{baseURL}/{updatedesign.Id}", updatedesign);
            var result = await httpResponse.GetResult();
            if (!httpResponse.Success || !result.IsSucceeded)
            {
                var errors = await httpResponse.GetErrors();
                throw new AppException(errors);
            }
            return result.Data;
        }
    }
}
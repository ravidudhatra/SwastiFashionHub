using SwastiFashionHub.Shared.Core.Http;
using SwastiFashionHub.Shared.Core.Services.Interface;
using SwastiFashionHub.Shared.Core.Exceptions;
using SwastiFashionHub.Data.Models;
using SwastiFashionHub.Common.Data.Response;
using SwastiFashionHub.Common.Data.Request;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            try
            {
                var formData = ConvertToFormData(design);

                var httpResponse = await httpService.Post($"{baseURL}", formData);
                var result = await httpResponse.GetResult();
                if (!httpResponse.Success || !result.IsSucceeded)
                {
                    var errors = await httpResponse.GetErrors();
                    throw new AppException(errors);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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


        public MultipartFormDataContent ConvertToFormData(object obj)
        {
            var formData = new MultipartFormDataContent();
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value != null)
                {
                    if (value is string || value is int || value is bool || value is decimal || value is double || value is Guid)
                    {
                        formData.Add(new StringContent(value.ToString()), property.Name);
                    }
                    else if (value is DateTime dateTimeValue)
                    {
                        formData.Add(new StringContent(dateTimeValue.ToString("o")), property.Name);
                    }
                    else if (value is IFormFile formFileValue)
                    {
                        var streamContent = new StreamContent(formFileValue.OpenReadStream());
                        formData.Add(streamContent, property.Name, formFileValue.FileName);
                    }
                    else if (value is IEnumerable<IFormFile> formFilesValue)
                    {
                        foreach (var file in formFilesValue)
                        {
                            var streamContent = new StreamContent(file.OpenReadStream());
                            formData.Add(streamContent, property.Name, file.FileName);
                        }
                    }
                    // Add other types as needed
                }
            }
            return formData;
        }

    }
}

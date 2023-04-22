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
    public interface IDesignService
    {
        Task<Result<DesignResponse>> GetAsync(Guid id);
        Task<Result<List<DesignResponse>>> GetAllAsync(string search = "");
        Task<Result<Guid>> SaveAsync(DesignRequest design);
        Task<Result<Guid>> UpdateAsync(DesignRequest design);
        Task<Result<Guid>> DeleteAsync(Guid id, string imagePath);

        Task<Result<Guid>> SaveDesignImageAsync(DesignImagesRequest designImages);
    }
}

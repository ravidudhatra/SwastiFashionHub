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
    public interface IDesignService
    {
        Task<DesignResponse> Get(Guid id);
        Task<List<DesignResponse>> GetAll();
        Task<object> Add(DesignRequest design);
        Task<object> Update(DesignRequest updatedesign);
        Task Delete(Guid id);
    }
}

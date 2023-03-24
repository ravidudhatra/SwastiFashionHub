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
        Task<Design> Get(Guid id);
        Task<List<Design>> GetAll();
        Task<object> Add(Design design);
        Task<object> Update(Design updatedesign);
        Task Delete(Guid id);
    }
}

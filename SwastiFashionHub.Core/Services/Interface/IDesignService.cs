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
        Task<Result<Design>> GetAsync(Guid id);
        Task<Result<List<Design>>> GetAllAsync(string search = "");
        Task<Result<Guid>> SaveAsync(Design design);
        Task<Result<Guid>> UpdateAsync(Design design);
        Task<Result<Guid>> DeleteAsync(Guid id);
    }
}

using SwastiFashionHub.Core.Wrapper;
using SwastiFashionHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Services.Interface
{
    public interface IPartyService
    {
        Task<Result<Party>> GetAsync(Guid id);
        Task<Result<List<Party>>> GetAllAsync(string search = "");
        Task<Result<Guid>> SaveAsync(Party design);
        Task<Result<Guid>> UpdateAsync(Party design);
        Task<Result<Guid>> DeleteAsync(Guid id);
    }
}

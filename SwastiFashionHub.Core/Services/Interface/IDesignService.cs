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
        Task<Result<Design>> GetDesignAsync(int id);
        Task<Result<List<Design>>> GetAllDesignAsync(string search = "");
        Task<Result<int>> SaveDesignAsync(Design design);
        Task<Result<int>> UpdateDesignAsync(Design design);
    }
}

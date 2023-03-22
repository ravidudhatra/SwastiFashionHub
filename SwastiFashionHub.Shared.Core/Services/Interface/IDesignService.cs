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
        Task<List<Design>> GetAll();
        Task<bool> Add(Design design);
        Task<bool> Update(Design updatedesign);
    }
}

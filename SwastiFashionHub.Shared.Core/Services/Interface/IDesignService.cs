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
        Task<bool> Post(Design design);
        Task<bool> Put(Design updatedesign);
    }
}

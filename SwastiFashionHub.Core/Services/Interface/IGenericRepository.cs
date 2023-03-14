using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Services.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        Task<bool> IsAnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}

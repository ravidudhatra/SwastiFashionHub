using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Core.Services.Interface;
using SwastiFashionHub.Data.Context;
using SwastiFashionHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SwastiFashionHubLlpContext _context;
        public GenericRepository(SwastiFashionHubLlpContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsQueryable();
        }

        public async Task<bool> IsAnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).AnyAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}

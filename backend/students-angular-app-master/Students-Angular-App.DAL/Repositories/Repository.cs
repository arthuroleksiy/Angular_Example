using Microsoft.EntityFrameworkCore;
using Students_Angular_App.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Students_Angular_App.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        StudentsAppContext _context;
        public Repository(StudentsAppContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(predicate);
            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public async Task CreateAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
           var item = await _context.Set<T>().FirstOrDefaultAsync(expression);
            if (item != null)
            {
                _context.Set<T>().Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllByAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task UpdateAsync(T item)
        {
             _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }
    }
}

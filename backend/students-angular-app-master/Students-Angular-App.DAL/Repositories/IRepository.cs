using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Students_Angular_App.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllByAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByAsync(Expression<Func<T, bool>> expression);

        Task CreateAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(Expression<Func<T, bool>> expression);

        
    }
}

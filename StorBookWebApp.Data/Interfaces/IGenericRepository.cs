using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace StorBookWebApp.Data.Implementation
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IPagedList<T>> GetAll(
                 RequestParams requestParams,
                 Expression<Func<T, bool>> expression = null,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                 List<string> includes = null
             );
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null);

        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        Task<T> Get1(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Delete(string id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}

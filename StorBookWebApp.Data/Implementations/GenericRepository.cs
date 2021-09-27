using Microsoft.EntityFrameworkCore;
using StorBookWebApp.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace StorBookWebApp.Data.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task Delete(string id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get1(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query.AsNoTracking();
        }

        public async Task<IPagedList<T>> GetAll(RequestParams requestParams, Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }


            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task<IList<T>> GetAll(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking()
                .ToListAsync();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }


            return query.AsNoTracking();
        }


        public async Task Add(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }
        public Task Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _db.Attach(entity); // Checks to see that the incoming record is different from existing record
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}


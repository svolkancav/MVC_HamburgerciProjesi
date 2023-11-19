using HamburgerciProject.Domain.Entities.BaseEntities;
using HamburgerciProject.Domain.Repositories;
using HamburgerciProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<T> _table;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync(); 
        }

        public async Task Delete(T entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table; // select * from Post
            if (where != null)
            {
                query = query.Where(where); // select * from Post where GenreId=3
            }
            if (include != null)
            {
                query = include(query); // join
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            }
            else
                return await query.Select(select).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table; // select * from Post
            if (where != null)
            {
                query = query.Where(where); // select * from Post where GenreId=3
            }
            if (include != null)
            {
                query = include(query); // join
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(select).ToListAsync();
            }
            else
                return await query.Select(select).ToListAsync();
        }

        public async Task Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await _table.FirstOrDefaultAsync(expression);
        }
    }
}

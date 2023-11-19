using HamburgerciProject.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class, IBaseEntity
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<TResult> GetFilteredFirstOrDefault<TResult>
           (
               Expression<Func<T, TResult>> select, // select
               Expression<Func<T, bool>> where, // where
               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, //sıralama
               Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null //join
           );

        Task<List<TResult>> GetFilteredList<TResult>
            (
                Expression<Func<T, TResult>> select, // select
                Expression<Func<T, bool>> where, // where
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, //sıralama
                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null //join
            );
    }
}

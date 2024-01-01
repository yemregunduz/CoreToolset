using CoreToolset.EntityFramework;
using CoreToolset.Extensions.Models.Pagination;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CoreToolset.EntityFramework.Repositories
{
    public interface IRepository<T> : IQuery<T> where T : BaseEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
             bool enableTracking = true, CancellationToken cancellationToken = default);
        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                        bool ignoreQueryFilters = false,
                                        int index = 0, int size = 10, bool enableTracking = true,
                                        CancellationToken cancellationToken = default);

        Task<IPaginate<T>> GetListByDynamicAsync(CoreToolset.Extensions.Models.DynamicQuery.Dynamic dynamic,
                                                 Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                 int index = 0, int size = 10, bool enableTracking = true,
                                                 CancellationToken cancellationToken = default);
        IQueryable<T> GetAsQueryable();

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<IList<T>> AddRangeAsync(IList<T> entity, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<T> DeleteByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IList<T> entities, CancellationToken cancellationToken = default);

        T Get(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = true);
        IList<T> GetAll(Expression<Func<T, bool>>? predicate = null,
                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                     Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                     bool enableTracking = true);
        IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
                             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                             int index = 0, int size = 10,
                             bool enableTracking = true);

        IPaginate<T> GetListByDynamic(CoreToolset.Extensions.Models.DynamicQuery.Dynamic dynamic,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                      int index = 0, int size = 10, bool enableTracking = true);

        T Add(T entity);
        IList<T> AddRange(IList<T> entities);
        T Update(T entity);
        T Delete(T entity);
        T DeleteById(int id);
        T DeleteByPredicate(Expression<Func<T, bool>> predicate);
        bool DeleteRange(IList<T> entities);

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

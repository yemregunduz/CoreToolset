using CoreToolset.Extensions.Models.Pagination;
using CoreToolset.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CoreToolset.EntityFramework.Repositories
{
    public class EfRepositoryBase<TEntity, TContext>(TContext context) : IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        protected TContext Context { get; } = context;

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            if (predicate is not null)
                return await queryable.Where(predicate)
                    .ToListAsync(cancellationToken);

            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();

            if (include is not null)
                queryable = include(queryable);
            if (!enableTracking)
                queryable = queryable.AsNoTracking();

            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                           bool ignoreQueryFilters = false,
                                                           int index = 0, int size = 10, bool enableTracking = true,
                                                           CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            if (predicate is not null)
                queryable = queryable.Where(predicate);
            if (ignoreQueryFilters)
                queryable = queryable.IgnoreQueryFilters();
            if (orderBy is not null)
                return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
            return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
        }

        public async Task<IPaginate<TEntity>> GetListByDynamicAsync(CoreToolset.Extensions.Models.DynamicQuery.Dynamic dynamic,
                                                                    Func<IQueryable<TEntity>,
                                                                    IIncludableQueryable<TEntity, object>>? include = null,
                                                                    int index = 0, int size = 10,
                                                                    bool enableTracking = true,
                                                                    CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamic);
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);

            return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
        }
        public IQueryable<TEntity> GetAsQueryable()
        {
            return Query().AsQueryable();
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Context.Entry(entity).State = EntityState.Added;
            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities, CancellationToken cancellationToken)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entityToUpdated = Context.Entry(entity);
            foreach (var property in entityToUpdated.Properties)
            {
                if (property.Metadata.Name.Equals("Id") || property.Metadata.Name.Equals("CreatedDate") || property.CurrentValue == null)
                {
                    property.IsModified = false;
                }
            }
            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> DeleteByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            TEntity? model = await GetAsync(predicate, cancellationToken: cancellationToken)
                ?? throw new Exception();
            return await DeleteAsync(model, cancellationToken);
        }

        public async Task<TEntity> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteByPredicateAsync(m => m.Id == id, cancellationToken);
        }

        public async Task<bool> DeleteRangeAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            await SaveChangesAsync(cancellationToken);
            return true;
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate,
                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                        bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query();

            if (include is not null)
                queryable = include(queryable);
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            return queryable.FirstOrDefault(predicate);
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null,
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                            bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            if (predicate is not null)
                return queryable.Where(predicate).ToList();
            return queryable.ToList();
        }

        public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                          int index = 0, int size = 10,
                                          bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            if (orderBy is not null)
                return orderBy(queryable).ToPaginate(index, size);
            return queryable.ToPaginate(index, size);
        }

        public IPaginate<TEntity> GetListByDynamic(CoreToolset.Extensions.Models.DynamicQuery.Dynamic dynamic,
                                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                       include = null, int index = 0, int size = 10,
                                                   bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamic);
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            return queryable.ToPaginate(index, size);
        }

        public TEntity Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            SaveChanges();
            return entity;
        }

        public IList<TEntity> AddRange(IList<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            SaveChanges();
            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            var entityToUpdated = Context.Entry(entity);
            foreach (var property in entityToUpdated.Properties)
            {
                if (!property.Metadata.Name.Equals("Id") && !property.Metadata.Name.Equals("CreatedDate"))
                {
                    property.IsModified = true;
                }
            }
            SaveChanges();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            SaveChanges();
            return entity;
        }
        public TEntity DeleteByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity? model = Get(predicate);
            return Delete(model);
        }

        public TEntity DeleteById(int id)
        {
            return DeleteByPredicate(m => m.Id == id);
        }

        public bool DeleteRange(IList<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            SaveChanges();
            return true;
        }
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }
    }
}

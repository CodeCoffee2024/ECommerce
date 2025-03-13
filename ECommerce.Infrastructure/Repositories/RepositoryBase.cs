using ECommerce.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    internal abstract class RepositoryBase<T>
        where T : BaseEntity
    {
        #region Fields

        protected readonly AppDbContext DbContext;

        #endregion Fields

        #region Protected Constructors

        protected RepositoryBase(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(Id, false, cancellationToken);
        }

        public virtual void Add(T entity)
        {
            DbContext.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            DbContext.Remove(entity);
        }

        internal IQueryable<T> GetAll()
        {
            return DbContext
                .Set<T>()
                .AsQueryable();
        }

        private async Task<T?> GetByIdAsync(
                                Guid Id,
        bool enableAsNoTracking,
        CancellationToken cancellationToken = default)
        {
            return enableAsNoTracking ?
                await DbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == Id, cancellationToken) :
                await DbContext
                .Set<T>()
                .FirstOrDefaultAsync(user => user.Id == Id, cancellationToken);
        }

        #endregion Protected Constructors
    }
}
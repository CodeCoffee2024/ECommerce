using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.UserManagement
{
    internal sealed class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        #region Public Constructors

        public ModuleRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<UnpagedResult<Module>> GetAllModules(DefaultFilterBaseDto listFilterDto)
        {
            var query = DbContext.Modules.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            var list = await query
                .OrderByDescending(r => r.Order)
                .AsNoTracking()
                .UnpaginateAsync(
                    listFilterDto.SortBy,
                    listFilterDto.SortDirection,
                    queryCount);

            return list;
        }

        #endregion Public Methods
    }
}
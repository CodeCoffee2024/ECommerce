using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;
using ECommerce.Domain.Entities.Settings;
using ECommerce.Domain.Entities.Settings.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.Settings
{
    internal sealed class UnitOfMeasurementTypeRepository : RepositoryBase<UnitOfMeasurementType>, IUnitOfMeasurementTypeRepository
    {
        #region Public Constructors

        public UnitOfMeasurementTypeRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Private Methods

        public UnitOfMeasurementType FindByName(string name)
        {
            var unitOfMeasurementType = DbContext.UnitOfMeasurementTypes
                .FirstOrDefault(it => it.Name.ToLower() == name.ToLower());
            return unitOfMeasurementType!;
        }

        public async Task<IEnumerable<UnitOfMeasurementType>> GetAllUnitOfMeasurementTypes(CancellationToken cancellationToken)
        {
            var result = DbContext.UnitOfMeasurementTypes.Where(it => it.Status == Status.Active.GetDescription()).AsEnumerable();
            return result;
        }

        public async Task<PagedResult<UnitOfMeasurementType>> GetListingPageResultAsync(UnitOfMeasurementTypeDTO listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.UnitOfMeasurementTypes.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(UnitOfMeasurementType =>
                  UnitOfMeasurementType.Name.Contains(listFilterDto.GlobalSearchValue) ||
                  UnitOfMeasurementType.CreatedBy.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  UnitOfMeasurementType.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
            }
            if (listFilterDto.HasDecimal != null)
            {
                query = query.Where(it => it.HasDecimal == (listFilterDto.HasDecimal == "Yes"));
            }
            if (listFilterDto.Status != null)
            {
                query = query.Where(UnitOfMeasurementType => UnitOfMeasurementType.Status == listFilterDto.Status);
            }

            var list = await query
                .OrderByDescending(r => r.ModifiedDate)
                .AsNoTracking()
                .PaginateAsync(
                    listFilterDto.Page,
                    listFilterDto.PageSize,
                    listFilterDto.SortBy,
                    listFilterDto.SortDirection,
                    queryCount);

            return list;
        }

        public async Task<PagedResult<UnitOfMeasurementType>> GetListingPageDropdownResultAsync(DefaultFilterBaseDto listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.UnitOfMeasurementTypes.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(UnitOfMeasurementType =>
                    UnitOfMeasurementType.Name.Contains(listFilterDto.GlobalSearchValue)
                );
                if (listFilterDto.Exclude != "")
                {
                    query = query.Where(UnitOfMeasurementType =>
                        UnitOfMeasurementType.Name.Contains(listFilterDto.GlobalSearchValue) && !listFilterDto.Exclude!.Contains(UnitOfMeasurementType.Name)
                    );
                }
            }

            var list = await query
                .OrderByDescending(r => r.ModifiedDate)
                .AsNoTracking()
                .PaginateAsync(
                    listFilterDto.Page,
                    listFilterDto.PageSize,
                    listFilterDto.SortBy,
                    listFilterDto.SortDirection,
                    queryCount);

            return list;
        }

        public async Task<UnpagedResult<UnitOfMeasurementType>> GetListingPageResultExportAsync(UnitOfMeasurementTypeDTO listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.UnitOfMeasurementTypes.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(UnitOfMeasurementType =>
                  UnitOfMeasurementType.Name.Contains(listFilterDto.GlobalSearchValue) ||
                  UnitOfMeasurementType.CreatedBy.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  UnitOfMeasurementType.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
            }
            if (listFilterDto.HasDecimal != null)
            {
                query = query.Where(it => it.HasDecimal == (listFilterDto.HasDecimal == "Yes"));
            }
            if (listFilterDto.Status != null)
            {
                query = query.Where(UnitOfMeasurementType => UnitOfMeasurementType.Status == listFilterDto.Status);
            }

            var list = await query
                .OrderByDescending(r => r.ModifiedDate)
                .AsNoTracking()
                .UnpaginateAsync(
                    listFilterDto.SortBy,
                    listFilterDto.SortDirection,
                    queryCount);

            return list;
        }

        public async Task<List<UnitOfMeasurementType>> GetListingResultAsync(
            string search, int? page, string sortBy, string sortDirection, string createdBy, CancellationToken cancellationToken = default)
        {
            const int pageSize = 10; // Default page size

            var query = DbContext.Set<UnitOfMeasurementType>().AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(up => up.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(createdBy))
            {
                query = query.Where(up => up.CreatedBy.LastName.Contains(createdBy) || up.CreatedBy.FirstName.Contains(createdBy));
            }

            // Sorting
            query = sortDirection.ToLower() switch
            {
                "desc" => query.OrderByDescending(up => EF.Property<object>(up, sortBy)),
                _ => query.OrderBy(up => EF.Property<object>(up, sortBy))
            };

            // Pagination
            var result = await query
                .Skip((page.Value - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return result;
        }

        #endregion Private Methods
    }
}
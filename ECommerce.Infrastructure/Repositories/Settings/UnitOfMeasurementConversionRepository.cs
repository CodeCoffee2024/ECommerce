using ECommerce.Domain.Entities.Settings;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Infrastructure.Repositories.Settings
{
    internal sealed class UnitOfMeasurementConversionRepository : RepositoryBase<UnitOfMeasurementConversion>, IUnitOfMeasurementConversionRepository
    {
        #region Public Constructors

        public UnitOfMeasurementConversionRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<UnitOfMeasurementConversion> GetCounterpartById(Guid FromId, Guid ToId, CancellationToken cancellationToken = default)
        {
            var unitOfMeasurement = DbContext.UnitOfMeasurementConversions
                     .FirstOrDefault(it => it.ConvertFromId == FromId && it.ConvertToId == ToId);
            return unitOfMeasurement!;
        }

        #endregion Public Methods

        //#region Private Methods

        //public UnitOfMeasurementConversion FindByName(string name)
        //{
        //    var unitOfMeasurement = DbContext.UnitOfMeasurements
        //        .Include(it => it.UnitOfMeasurementType)
        //        .FirstOrDefault(it => it.Name.ToLower() == name.ToLower());
        //    return unitOfMeasurement!;
        //}

        //public UnitOfMeasurementConversion FindByAbbreviation(string abbreviation)
        //{
        //    var unitOfMeasurement = DbContext.UnitOfMeasurements
        //        .FirstOrDefault(it => it.Abbreviation.ToLower() == abbreviation.ToLower());
        //    return unitOfMeasurement!;
        //}

        //public async Task<IEnumerable<UnitOfMeasurementConversion>> GetAllUnitOfMeasurements(CancellationToken cancellationToken)
        //{
        //    var result = DbContext.UnitOfMeasurements.Where(it => it.Status == Status.Active.GetDescription()).AsEnumerable();
        //    return result;
        //}

        //public async Task<PagedResult<UnitOfMeasurementConversion>> GetListingPageResultAsync(UnitOfMeasurementDTO listFilterDto, CancellationToken cancellationToken)
        //{
        //    var query = DbContext.UnitOfMeasurements.AsQueryable();
        //    var queryCount = await query.AsNoTracking().CountAsync();

        //    if (listFilterDto.HasSearchValues)
        //    {
        //        query = query.Where(UnitOfMeasurement =>
        //          UnitOfMeasurement.Name.Contains(listFilterDto.GlobalSearchValue) ||
        //          UnitOfMeasurement.UnitOfMeasurementType.Name.Contains(listFilterDto.GlobalSearchValue) ||
        //          UnitOfMeasurement.CreatedBy.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
        //          UnitOfMeasurement.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
        //    }
        //    if (listFilterDto.Status != null)
        //    {
        //        query = query.Where(UnitOfMeasurement => UnitOfMeasurement.Status == listFilterDto.Status);
        //    }

        //    var list = await query
        //        .OrderByDescending(r => r.ModifiedDate)
        //        .AsNoTracking()
        //        .PaginateAsync(
        //            listFilterDto.Page,
        //            listFilterDto.PageSize,
        //            listFilterDto.SortBy,
        //            listFilterDto.SortDirection,
        //            queryCount);

        //    return list;
        //}

        //public async Task<UnpagedResult<UnitOfMeasurementConversion>> GetListingPageResultExportAsync(UnitOfMeasurementDTO listFilterDto, CancellationToken cancellationToken)
        //{
        //    var query = DbContext.UnitOfMeasurements.AsQueryable();
        //    var queryCount = await query.AsNoTracking().CountAsync();

        //    if (listFilterDto.HasSearchValues)
        //    {
        //        query = query.Where(UnitOfMeasurement =>
        //          UnitOfMeasurement.Name.Contains(listFilterDto.GlobalSearchValue) ||
        //          UnitOfMeasurement.UnitOfMeasurementType.Name.Contains(listFilterDto.GlobalSearchValue) ||
        //          UnitOfMeasurement.CreatedBy!.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
        //          UnitOfMeasurement.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
        //    }
        //    if (listFilterDto.Status != null)
        //    {
        //        query = query.Where(UnitOfMeasurement => UnitOfMeasurement.Status == listFilterDto.Status);
        //    }

        //    var list = await query
        //        .OrderByDescending(r => r.ModifiedDate)
        //        .AsNoTracking()
        //        .UnpaginateAsync(
        //            listFilterDto.SortBy,
        //            listFilterDto.SortDirection,
        //            queryCount);

        //    return list;
        //}

        //public async Task<List<UnitOfMeasurement>> GetListingResultAsync(
        //    string search, int? page, string sortBy, string sortDirection, string createdBy, CancellationToken cancellationToken = default)
        //{
        //    const int pageSize = 10; // Default page size

        //    var query = DbContext.Set<UnitOfMeasurement>().AsQueryable();

        //    // Filtering
        //    if (!string.IsNullOrWhiteSpace(search))
        //    {
        //        query = query.Where(up => up.Name.Contains(search));
        //    }

        //    if (!string.IsNullOrWhiteSpace(createdBy))
        //    {
        //        query = query.Where(up => up.CreatedBy.LastName.Contains(createdBy) || up.CreatedBy.FirstName.Contains(createdBy));
        //    }

        //    // Sorting
        //    query = sortDirection.ToLower() switch
        //    {
        //        "desc" => query.OrderByDescending(up => EF.Property<object>(up, sortBy)),
        //        _ => query.OrderBy(up => EF.Property<object>(up, sortBy))
        //    };

        //    // Pagination
        //    var result = await query
        //        .Skip((page.Value - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync(cancellationToken);

        //    return result;
        //}

        //#endregion Private Methods
    }
}
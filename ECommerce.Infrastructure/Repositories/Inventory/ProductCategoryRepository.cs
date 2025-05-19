using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Inventory.ProductCategory;
using ECommerce.Domain.Entities.Inventory;
using ECommerce.Domain.Entities.Inventory.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.Inventory
{
    internal sealed class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        #region Public Constructors

        public ProductCategoryRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Private Methods

        public ProductCategory FindByName(string name)
        {
            var productCategory = DbContext.ProductCategories
                .FirstOrDefault(it => it.Name.ToLower() == name.ToLower());
            return productCategory!;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategories(CancellationToken cancellationToken)
        {
            var result = DbContext.ProductCategories.Where(it => it.Status == Status.Active.GetDescription()).AsEnumerable();
            return result;
        }

        public async Task<PagedResult<ProductCategory>> GetListingPageResultAsync(ProductCategoryDTO listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.ProductCategories.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(ProductCategory =>
                  ProductCategory.Name.Contains(listFilterDto.GlobalSearchValue) ||
                  ProductCategory.CreatedBy!.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  ProductCategory.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
            }
            if (listFilterDto.Status != null)
            {
                query = query.Where(ProductCategory => ProductCategory.Status == listFilterDto.Status);
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

        public async Task<PagedResult<ProductCategory>> GetListingPageDropdownResultAsync(ProductCategoryDTO listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.ProductCategories.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(ProductCategory =>
                    ProductCategory.Name.Contains(listFilterDto.GlobalSearchValue) && ProductCategory.Status == "activ"
                );
                if (listFilterDto.Exclude != "")
                {
                    query = query.Where(ProductCategory =>
                        ProductCategory.Name.Contains(listFilterDto.GlobalSearchValue) && !listFilterDto.Exclude!.Contains(ProductCategory.Name)
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

        public async Task<UnpagedResult<ProductCategory>> GetListingPageResultExportAsync(ProductCategoryDTO listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.ProductCategories.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(ProductCategory =>
                  ProductCategory.Name.Contains(listFilterDto.GlobalSearchValue) ||
                  ProductCategory.CreatedBy.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  ProductCategory.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
            }
            if (listFilterDto.Status != null)
            {
                query = query.Where(ProductCategory => ProductCategory.Status == listFilterDto.Status);
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

        public async Task<List<ProductCategory>> GetListingResultAsync(
            string search, int? page, string sortBy, string sortDirection, string createdBy, CancellationToken cancellationToken = default)
        {
            const int pageSize = 10; // Default page size

            var query = DbContext.Set<ProductCategory>().AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(up => up.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(createdBy))
            {
                query = query.Where(up => up.CreatedBy.LastName.Contains(createdBy) || up.CreatedBy.FirstName.Contains(createdBy));
            }

            query = sortDirection.ToLower() switch
            {
                "desc" => query.OrderByDescending(up => EF.Property<object>(up, sortBy)),
                _ => query.OrderBy(up => EF.Property<object>(up, sortBy))
            };

            var result = await query
                .Skip((page.Value - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return result;
        }

        #endregion Private Methods
    }
}
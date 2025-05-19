using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.Inventory.ProductCategory;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategory
{
    public class GetProductCategoryRequest : FilterBaseDto
    {
        #region Public Methods

        public string? Status { get; set; }

        public TQuery SetQuery<TQuery>() where TQuery : GetProductCategoryRequest, new()
        {
            return new TQuery
            {
                Search = Search,
                Status = Status,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy
            };
        }

        internal ProductCategoryDTO SetGlobalSearchValueFilterDTO()
        {
            var searchValues = new Dictionary<string, string>();
            var status = Status;
            if (!string.IsNullOrWhiteSpace(Search))
                searchValues.Add(GlobalConstant.SEARCH_VALUE, Search);

            return new ProductCategoryDTO()
            {
                SearchValues = searchValues,
                Status = status!,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
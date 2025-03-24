using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;

namespace ECommerce.Application.CommandQueries.Common
{
    public class GenericListingRequest : FilterBaseDto
    {
        #region Public Methods

        public TQuery SetQuery<TQuery>() where TQuery : GenericListingRequest, new()
        {
            return new TQuery
            {
                Search = Search,
                Page = Page,
                PageSize = PageSize,
                SortDirection = SortDirection,
                SortBy = SortBy
            };
        }

        internal DefaultFilterBaseDto SetGlobalSearchValueFilterDTO()
        {
            var searchValues = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(Search))
                searchValues.Add(GlobalConstant.SEARCH_VALUE, Search);

            return new DefaultFilterBaseDto()
            {
                SearchValues = searchValues,
                Page = Page,
                PageSize = PageSize,
                SortDirection = SortDirection,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
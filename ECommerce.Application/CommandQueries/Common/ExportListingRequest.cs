using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;

namespace ECommerce.Application.CommandQueries.Common
{
    public class ExportListingRequest : FilterBaseDto
    {
        #region Public Methods

        public TQuery SetQuery<TQuery>() where TQuery : ExportListingRequest, new()
        {
            return new TQuery
            {
                Search = Search,
                SortDirection = SortDirection,
                ReportName = ReportName,
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
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
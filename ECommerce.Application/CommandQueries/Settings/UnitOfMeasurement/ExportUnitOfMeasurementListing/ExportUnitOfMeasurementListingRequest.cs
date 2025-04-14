using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.ExportUnitOfMeasurementListing
{
    public class ExportUnitOfMeasurementListingRequest : FilterBaseDto
    {
        #region Public Methods

        public string? Status { get; set; }

        public TQuery SetQuery<TQuery>() where TQuery : ExportUnitOfMeasurementListingRequest, new()
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

        internal UnitOfMeasurementDTO SetGlobalSearchValueFilterDTO()
        {
            var searchValues = new Dictionary<string, string>();
            var status = Status;
            if (!string.IsNullOrWhiteSpace(Search))
                searchValues.Add(GlobalConstant.SEARCH_VALUE, Search);

            return new UnitOfMeasurementDTO()
            {
                SearchValues = searchValues,
                Status = status,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
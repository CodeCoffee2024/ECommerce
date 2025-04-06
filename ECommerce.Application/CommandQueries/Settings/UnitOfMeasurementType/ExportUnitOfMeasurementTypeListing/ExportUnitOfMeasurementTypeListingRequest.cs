using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.ExportUnitOfMeasurementTypeListing
{
    public class ExportUnitOfMeasurementTypeListingRequest : FilterBaseDto
    {
        #region Public Methods

        public string? Status { get; set; }
        public string? HasDecimal { get; set; }

        public TQuery SetQuery<TQuery>() where TQuery : ExportUnitOfMeasurementTypeListingRequest, new()
        {
            return new TQuery
            {
                Search = Search,
                Status = Status,
                HasDecimal = HasDecimal,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy
            };
        }

        internal UnitOfMeasurementTypeDTO SetGlobalSearchValueFilterDTO()
        {
            var searchValues = new Dictionary<string, string>();
            var hasDecimal = HasDecimal;
            var status = Status;
            if (!string.IsNullOrWhiteSpace(Search))
                searchValues.Add(GlobalConstant.SEARCH_VALUE, Search);

            return new UnitOfMeasurementTypeDTO()
            {
                SearchValues = searchValues,
                Status = status,
                HasDecimal = hasDecimal,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
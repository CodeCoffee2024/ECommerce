using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementType
{
    public class GetUnitOfMeasurementTypeRequest : FilterBaseDto
    {
        #region Public Methods

        public string? Status { get; set; }
        public string? HasDecimal { get; set; } = string.Empty;

        public TQuery SetQuery<TQuery>() where TQuery : GetUnitOfMeasurementTypeRequest, new()
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
            var status = Status;
            var hasDecimal = HasDecimal;
            if (!string.IsNullOrWhiteSpace(Search))
                searchValues.Add(GlobalConstant.SEARCH_VALUE, Search);

            return new UnitOfMeasurementTypeDTO()
            {
                SearchValues = searchValues,
                Status = status!,
                SortDirection = SortDirection,
                HasDecimal = hasDecimal,
                ReportName = ReportName,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
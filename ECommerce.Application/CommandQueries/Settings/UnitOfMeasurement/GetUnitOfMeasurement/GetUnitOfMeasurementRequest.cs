using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurement
{
    public class GetUnitOfMeasurementRequest : FilterBaseDto
    {
        #region Public Methods

        public string? Status { get; set; }
        public Guid? UnitOfMeasurementType { get; set; }

        public TQuery SetQuery<TQuery>() where TQuery : GetUnitOfMeasurementRequest, new()
        {
            return new TQuery
            {
                Search = Search,
                Status = Status,
                UnitOfMeasurementType = UnitOfMeasurementType,
                Exclude = Exclude,
                Page = Page,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy
            };
        }

        internal UnitOfMeasurementDTO SetGlobalSearchValueFilterDTO()
        {
            var searchValues = new Dictionary<string, string>();
            var status = Status;
            var unitOfMeasurementType = UnitOfMeasurementType;
            var exclude = Exclude;
            if (!string.IsNullOrWhiteSpace(Search))
                searchValues.Add(GlobalConstant.SEARCH_VALUE, Search);

            return new UnitOfMeasurementDTO()
            {
                SearchValues = searchValues,
                UnitOfMeasurementType = unitOfMeasurementType!,
                Status = status!,
                Page = Page,
                Exclude = exclude,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
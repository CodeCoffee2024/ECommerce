using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;

namespace ECommerce.Domain.Dtos.Settings.UnitOfMeasurement
{
    public class UnitOfMeasurementDTO : FilterBaseDto
    {
        #region Properties

        public Dictionary<string, string> SearchValues { get; set; } = new Dictionary<string, string>();
        public bool HasSearchValues { get => SearchValues.Any(); }
        public string Status { get; set; } = string.Empty;
        public string GlobalSearchValue { get => SearchValues.GetValueOrDefault(GlobalConstant.SEARCH_VALUE)?.ToString().Trim() ?? string.Empty; }

        #endregion Properties
    }
}
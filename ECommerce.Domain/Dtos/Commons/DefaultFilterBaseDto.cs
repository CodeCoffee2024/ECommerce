using ECommerce.Domain.Commons.Constants;

namespace ECommerce.Domain.Dtos.Commons
{
    public class DefaultFilterBaseDto : FilterBaseDto
    {
        #region Properties

        public Dictionary<string, string> SearchValues { get; set; } = new Dictionary<string, string>();
        public bool HasSearchValues { get => SearchValues.Any(); }
        public string GlobalSearchValue { get => SearchValues.GetValueOrDefault(GlobalConstant.SEARCH_VALUE)?.ToString().Trim() ?? string.Empty; }

        #endregion Properties
    }
}
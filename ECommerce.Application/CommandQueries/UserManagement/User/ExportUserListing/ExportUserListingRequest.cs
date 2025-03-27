using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.UserManagement.User;

namespace ECommerce.Application.CommandQueries.UserManagement.User.ExportUserListing
{
    public class ExportUserListingRequest : FilterBaseDto
    {
        #region Public Methods

        public string? UserPermissions { get; set; }

        public TQuery SetQuery<TQuery>() where TQuery : ExportUserListingRequest, new()
        {
            return new TQuery
            {
                Search = Search,
                UserPermissions = UserPermissions,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy
            };
        }

        internal UserListingDTO SetGlobalSearchValueFilterDTO()
        {
            var searchValues = new Dictionary<string, string>();
            var permissions = UserPermissions;
            if (!string.IsNullOrWhiteSpace(Search))
                searchValues.Add(GlobalConstant.SEARCH_VALUE, Search);

            return new UserListingDTO()
            {
                SearchValues = searchValues,
                UserPermissions = UserPermissions,
                SortDirection = SortDirection,
                ReportName = ReportName,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
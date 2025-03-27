using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.UserManagement.User;

namespace ECommerce.Application.CommandQueries.UserManagement.User
{
    public class UserListingRequest : FilterBaseDto
    {
        #region Public Methods

        public string? UserPermissions { get; set; } = string.Empty;

        public TQuery SetQuery<TQuery>() where TQuery : UserListingRequest, new()
        {
            return new TQuery
            {
                Search = Search,
                Page = Page,
                UserPermissions = UserPermissions,
                PageSize = PageSize,
                SortDirection = SortDirection,
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
                UserPermissions = permissions,
                Page = Page,
                PageSize = PageSize,
                SortDirection = SortDirection,
                SortBy = SortBy,
            };
        }

        #endregion Public Methods
    }
}
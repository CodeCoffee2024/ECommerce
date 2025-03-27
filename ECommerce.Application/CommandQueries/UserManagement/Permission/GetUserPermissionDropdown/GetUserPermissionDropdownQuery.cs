using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermissionDropdown
{
    public class GetUserPermissionDropdownQuery : GenericListingRequest, IQuery<PagedResult<GetUserPermissionDropdownResponse>>
    {
    }
}
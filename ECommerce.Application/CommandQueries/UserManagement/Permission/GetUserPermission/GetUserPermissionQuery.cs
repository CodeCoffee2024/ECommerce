using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermission
{
    public class GetUserPermissionQuery : GenericListingRequest, IQuery<PagedResult<GetUserPermissionResponse>>
    {
    }
}
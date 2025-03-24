using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.ExportUserPermission
{
    public class ExportUserPermissionQuery : ExportListingRequest, IQuery<UnpagedResult<ExportUserPermissionResponse>>
    {
    }
}
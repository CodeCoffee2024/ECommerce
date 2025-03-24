using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetOneUserPermission
{
    public sealed record GetOneUserPermissionQuery(Guid Id) : IQuery<GetOneUserPermissionResponse>;
}
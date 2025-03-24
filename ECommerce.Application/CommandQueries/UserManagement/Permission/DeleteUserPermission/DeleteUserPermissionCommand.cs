using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.DeleteUserPermission
{
    public sealed record DeleteUserPermissionCommand(Guid Id) : ICommand;
}
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.UserManagement.Permission.Validators;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.UpdateUserPermission
{
    public sealed record UpdateUserPermissionCommand(
        string Name,
        string Permissions,
        Guid Id,
        Guid UpdatedById) : ICommand, IUpdateUserPermissionCommand;
}
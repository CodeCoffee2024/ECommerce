using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.UserManagement.Permission.Validators;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.AddUserPermission
{
    public sealed record AddUserPermissionCommand(
        string Name,
        string Permissions,
        Guid Id) : ICommand, IAddUserPermissionCommand;
}
using ECommerce.Application.CommandQueries.UserManagement.Permission.AddUserPermission;
using ECommerce.Application.CommandQueries.UserManagement.Permission.UpdateUserPermission;

namespace ECommerce.Api.Controllers.UserManagement
{
    public sealed record UserPermissionRequest
    {
        public string Name { get; init; } = string.Empty;
        public string Permissions { get; init; } = string.Empty;
        public AddUserPermissionCommand SetAddCommand(Guid userId) =>
            new(Name, Permissions, userId);
        public UpdateUserPermissionCommand SetUpdateCommand(Guid userId, Guid updateById) =>
            new(Name, Permissions, userId, updateById);
    }
}
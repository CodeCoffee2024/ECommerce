using ECommerce.Application.CommandQueries.UserManagement.User.AddUser;
using ECommerce.Application.CommandQueries.UserManagement.User.UpdateUser;

namespace ECommerce.Api.Controllers.UserManagement.User
{
    public sealed record UserRequest
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserPermissions { get; set; } = string.Empty;
        public AddUserCommand SetAddCommand(Guid userId) =>
            new(LastName, FirstName, MiddleName!, BirthDate, Email, UserName, Password, UserPermissions, userId);

        public UpdateUserCommand SetUpdateCommand(Guid userId, Guid updateById) =>
            new(LastName, FirstName, MiddleName!, BirthDate, Email, UserName, UserPermissions, userId, updateById);
    }
}
using ECommerce.Application.CommandQueries.UserManagement.User.AddUser;
using ECommerce.Application.CommandQueries.UserManagement.User.UpdateUser;
using ECommerce.Application.CommandQueries.UserManagement.User.UpdateUserProfile;

namespace ECommerce.Api.Controllers.UserManagement.User
{
    public sealed record UserRequest
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string UserName { get; set; } = string.Empty;
        public IFormFile? Img { get; set; } = default!;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserPermissions { get; set; } = string.Empty;
        public AddUserCommand SetAddCommand(Guid userId) =>
            new(LastName, FirstName, MiddleName!, BirthDate, Email, UserName, Password, UserPermissions, userId);

        public UpdateUserCommand SetUpdateCommand(Guid userId, Guid updateById) =>
            new(LastName, FirstName, MiddleName!, BirthDate, Email, UserName, UserPermissions, userId, updateById);

        public UpdateUserProfileCommand SetUpdateProfileCommand(Guid userId, Guid updateById) =>
            new(LastName, FirstName, MiddleName!, BirthDate, UserName, Img, userId, updateById);
    }
}
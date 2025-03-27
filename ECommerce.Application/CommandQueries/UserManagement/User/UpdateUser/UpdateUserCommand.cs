using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.UserManagement.User.Validators;

namespace ECommerce.Application.CommandQueries.UserManagement.User.UpdateUser
{
    public sealed record UpdateUserCommand(
        string LastName,
        string FirstName,
        string MiddleName,
        DateTime? BirthDate,
        string Email,
        string UserName,
        string UserPermissions,
        Guid Id,
        Guid UpdatedById) : ICommand, IUpdateUserValidator;
}
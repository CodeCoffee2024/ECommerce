using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.UserManagement.User.Validators;

namespace ECommerce.Application.CommandQueries.UserManagement.User.AddUser
{
    public sealed record AddUserCommand(
        string LastName,
        string FirstName,
        string MiddleName,
        DateTime? BirthDate,
        string Email,
        string UserName,
        string Password,
        string UserPermissions,
        Guid Id) : ICommand, IAddUserValidator;
}
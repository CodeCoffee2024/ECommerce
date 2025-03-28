using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.UserManagement.User.Validators;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.CommandQueries.UserManagement.User.UpdateUserProfile
{
    public sealed record UpdateUserProfileCommand(
        string LastName,
        string FirstName,
        string MiddleName,
        DateTime? BirthDate,
        string UserName,
        IFormFile Img,
        Guid Id,
        Guid UpdatedById) : ICommand, IUpdateUserProfileCommand;
}
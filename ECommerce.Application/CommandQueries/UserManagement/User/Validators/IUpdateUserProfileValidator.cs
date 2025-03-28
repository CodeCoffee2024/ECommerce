using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.CommandQueries.UserManagement.User.Validators
{
    public interface IUpdateUserProfileCommand
    {
        #region Properties

        Guid Id { get; }
        string LastName { get; }
        string FirstName { get; }
        string? MiddleName { get; }
        string UserName { get; }
        DateTime? BirthDate { get; }
        IFormFile Img { get; }

        #endregion Properties
    }
}
namespace ECommerce.Application.CommandQueries.UserManagement.User.Validators
{
    public interface IAddUserValidator
    {
        #region Properties

        Guid Id { get; }
        string LastName { get; }
        string FirstName { get; }
        string? MiddleName { get; }
        string Email { get; }
        string UserName { get; }
        string Password { get; }
        DateTime? BirthDate { get; }
        string UserPermissions { get; }

        #endregion Properties
    }
}
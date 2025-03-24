namespace ECommerce.Application.CommandQueries.UserManagement.Permission.Validators
{
    public interface IUpdateUserPermissionCommand
    {
        #region Properties

        Guid Id { get; }
        string Name { get; }
        string Permissions { get; }

        #endregion Properties
    }
}
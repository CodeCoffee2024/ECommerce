namespace ECommerce.Application.CommandQueries.UserManagement.Permission.Validators
{
    public interface IAddUserPermissionCommand
    {
        #region Properties

        Guid Id { get; }
        string Name { get; }
        string Permissions { get; }

        #endregion Properties
    }
}
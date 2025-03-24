namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermission
{
    public sealed class UserPermissionFragmentDTO
    {
        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public string Permissions { get; set; }
        public string Dependencies { get; set; }

        #endregion Properties
    }
}
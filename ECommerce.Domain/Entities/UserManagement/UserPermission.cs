using ECommerce.Domain.Abstractions;

namespace ECommerce.Domain.Entities.UserManagement
{
    public class UserPermission : AuditableEntity
    {
        #region Properties

        public UserPermission()
        {
        }

        private UserPermission(string permissions, string name)
        {
            Permissions = permissions;
            Name = name;
        }

        public virtual ICollection<UserUserPermission> UserUserPermissions { get; private set; } = new List<UserUserPermission>();
        public string Name { get; private set; } = string.Empty;
        public string Permissions { get; private set; } = string.Empty;

        public static UserPermission Create(string permissions, string name, Guid? createdBy, DateTime createdDate)
        {
            var userPermission = new UserPermission(permissions, name);
            userPermission.SetCreated(createdDate, createdBy);
            return userPermission;
        }

        #endregion Properties
    }
}
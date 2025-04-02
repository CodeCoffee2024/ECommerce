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

        public UserPermission Update(string permissions, string name, Guid? createdBy, DateTime createdDate)
        {
            Permissions = permissions;
            Name = name;
            SetUpdated(createdDate, createdBy);
            return this;
        }

        public Dictionary<string, string> GetActivityLog(string modifiedBy = "", string createdBy = "")
        {
            return new Dictionary<string, string>
            {
                { "Name", Name },
                { "Permissions", Permissions },
                { "Modified By", !string.IsNullOrEmpty(modifiedBy) ? modifiedBy : ModifiedBy?.FirstName + " " + ModifiedBy?.LastName },
                { "Created By",!string.IsNullOrEmpty(createdBy) ? createdBy : CreatedBy?.FirstName + " " + CreatedBy?.LastName }
            };
        }

        #endregion Properties
    }
}
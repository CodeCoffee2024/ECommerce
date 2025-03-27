using ECommerce.Domain.Abstractions;

namespace ECommerce.Domain.Entities.UserManagement
{
    public class User : AuditableEntity
    {
        #region Properties

        public virtual ICollection<UserUserPermission>? UserUserPermissions { get; set; } = new List<UserUserPermission>();

        public string LastName { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string MiddleName { get; private set; } = string.Empty;
        public DateTime? BirthDate { get; private set; }

        #endregion Properties

        #region Private Constructors

        public User()
        { }

        private User(string lastName, string firstName, string middleName, DateTime? birthDate, string email, string username, string password)
        {
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
        }

        public bool isSuperAdmin()
        {
            return Username == "admin";
        }

        public User Update(string lastName, string firstName, string middleName, DateTime? birthDate, string email, string username, DateTime? updatedDate, Guid updatedById)
        {
            LastName = lastName;
            Email = email;
            Username = username;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
            SetUpdated(updatedDate, updatedById);
            return this;
        }

        #endregion Private Constructors

        #region Private Methods

        public static User Create(string lastName, string firstName, string middleName, DateTime? birthDate, string email, string username, string password, DateTime? createdDate, Guid? createdById)
        {
            var user = new User(lastName, firstName, middleName, birthDate, email, username, password);
            user.SetCreated(createdDate, createdById);
            return user;
        }

        public void SetUserUserPermissions(ICollection<UserUserPermission> userUserPermissions)
        {
            UserUserPermissions = userUserPermissions;
        }

        #endregion Private Methods
    }
}
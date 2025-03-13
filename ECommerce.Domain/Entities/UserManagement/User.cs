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

        private User(string lastName, string firstName, string middleName, DateTime? birthDate, string email, string username, string password, ICollection<UserUserPermission>? userUserPermissions)
        {
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
            UserUserPermissions = userUserPermissions;
        }

        #endregion Private Constructors

        #region Private Methods

        public static User Create(string lastName, string firstName, string middleName, DateTime? birthDate, string email, string username, string password, ICollection<UserUserPermission>? userUserPermissions, DateTime? createdDate, Guid? createdById)
        {
            var user = new User(lastName, firstName, middleName, birthDate, email, username, password, userUserPermissions);
            user.SetCreated(createdDate, createdById);
            return user;
        }

        #endregion Private Methods
    }
}
using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Infrastructure.Seeders.Scripts
{
    public class UserSeeder
    {
        #region Internal Methods

        internal static void Run(AppDbContext context, string hashedPassword)
        {
            var users = new List<User>
            {
                User.Create("Admin Lastname", "Admin Firstname", "Admin middlename",DateTime.Now, "admin@gmail.com","admin", hashedPassword, DateTime.UtcNow, null),
                User.Create("Staff Lastname", "Staff Firstname", "Staff middlename",DateTime.Now, "Staff@gmail.com","Staff", hashedPassword, DateTime.UtcNow, null),
            };

            foreach (var user in users)
            {
                var existingUser = context.Set<User>().FirstOrDefault(u => u.Email == user.Email);
                if (existingUser == null)
                {
                    context.Set<User>().Add(user);
                }
                context.SaveChanges();
            }
        }

        #endregion Internal Methods
    }
}
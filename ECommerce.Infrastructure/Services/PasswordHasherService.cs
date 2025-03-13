using ECommerce.Application.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        #region Fields

        private readonly PasswordHasher<object> _passwordHasher;

        #endregion Fields

        #region Public Constructors

        public PasswordHasherService()
        {
            _passwordHasher = new PasswordHasher<object>();
        }

        #endregion Public Constructors

        #region Public Methods

        public string HashPassword(string password)
        {
            // Hash the password using PasswordHasher
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            // Verify the password
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        #endregion Public Methods
    }
}
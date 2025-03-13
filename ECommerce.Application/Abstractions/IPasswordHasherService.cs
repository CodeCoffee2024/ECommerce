namespace ECommerce.Application.Abstractions
{
    public interface IPasswordHasherService
    {
        #region Public Methods

        string HashPassword(string password);

        bool VerifyPassword(string hashedPassword, string providedPassword);

        #endregion Public Methods
    }
}
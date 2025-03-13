using ECommerce.Domain.Abstractions;

namespace ECommerce.Domain.Entities.UserManagement
{
    public static class UserErrors
    {
        #region Fields

        public static readonly Error NotFound = new(
            "User.NotFound",
            "The user with the specified identifier was not found");

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "Password is incorrect.");

        public static readonly Error RefreshTokenRequired = new(
            "User.RefreshTokenRequired",
            "The user with the refresh token is required");

        public static readonly Error InvalidEmail = new(
          "User.InvalidEmail",
          "Invalid email!");

        #endregion Fields
    }
}
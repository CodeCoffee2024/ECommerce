using ECommerce.Application.CommandQueries.Auth.Login;

namespace ECommerce.Api.Controllers.Auth
{
    public sealed record AuthRequest
    {
        #region Properties

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public LoginQuery Login() =>
            new LoginQuery(Username, Password);

        #endregion Properties
    }
}
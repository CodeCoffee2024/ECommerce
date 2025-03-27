namespace ECommerce.Api.Controllers.Auth
{
    public sealed record AuthRefreshTokenRequest
    {
        #region Properties

        public string RefreshToken { get; set; } = string.Empty;

        #endregion Properties
    }
}
namespace ECommerce.Application.CommandQueries.Security
{
    public sealed class TokenResponse
    {
        #region Properties

        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresInUtc { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiresInUtc { get; set; }

        #endregion Properties
    }
}
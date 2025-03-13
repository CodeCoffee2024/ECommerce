using ECommerce.Application.Abstractions;
using ECommerce.Application.CommandQueries.Security;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        #region Fields

        protected readonly string Key;
        protected readonly string Issuer;
        protected readonly string Audience;
        private readonly int _accessTokenExpiration;
        private readonly int _refreshTokenExpiration;
        private readonly IUserRepository _userRepository;

        #endregion Fields

        #region Public Constructors

        public TokenService(IConfiguration configuration, IUserRepository userRepository)
        {
            _accessTokenExpiration = int.Parse(configuration["JwtSettings:AccessTokenExpiration"]);
            _refreshTokenExpiration = int.Parse(configuration["JwtSettings:RefreshTokenExpiration"]);
            Key = configuration["JwtSettings:SecretKey"];
            Issuer = configuration["JwtSettings:Issuer"];
            Audience = configuration["JwtSettings:Audience"];
            _userRepository = userRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public TokenResponse GenerateToken(User user)
        {
            TokenResponse response = new TokenResponse();

            var commonClaims = GetCommonClaims(user);

            response.ExpiresInUtc = DateTime.UtcNow.AddMinutes(_accessTokenExpiration);
            response.RefreshTokenExpiresInUtc = DateTime.UtcNow.AddMinutes(_refreshTokenExpiration);

            // Generate Access Token
            response.AccessToken = GenerateJwtToken(commonClaims, response.ExpiresInUtc);

            // Generate Refresh Token
            response.RefreshToken = GenerateJwtToken(commonClaims, response.RefreshTokenExpiresInUtc);

            return response;
        }

        public TokenResponse RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var principal = GetPrincipalFromExpiredToken(refreshToken);
            if (principal == null)
            {
                return null!;
            }

            var userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                return null!;
                throw new SecurityTokenException("Invalid refresh token");
            }

            var user = _userRepository.GetByIdAsync(Guid.Parse(userId), cancellationToken).Result;

            if (user == null)
            {
                return null!;
            }

            return GenerateToken(user);
        }

        #endregion Public Methods

        #region Private Methods

        protected SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetCommonClaims(User user)
        {
            var permissions = new List<string>();
            if (user.Email != "admin@gmail.com")
            {
                permissions = user.UserUserPermissions!.Select(it => it.UserPermission.Permissions).ToList();
            }

            // Convert the permissions list to a comma-separated string
            var permissionsString = string.Join(",", permissions);

            return new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("UserId", user.Id.ToString()),
            new Claim("Permissions", permissionsString)
        };
        }

        private string GenerateJwtToken(List<Claim> commonClaims, DateTime expiration)
        {
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: commonClaims,
                expires: expiration,
                signingCredentials: GetSigningCredentials());

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (securityToken == null || securityToken.ValidTo < DateTime.UtcNow)
                {
                    return null; // Token is expired or invalid
                }

                var identity = new ClaimsIdentity(securityToken.Claims, "jwt");
                var principal = new ClaimsPrincipal(identity);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        #endregion Private Methods
    }
}
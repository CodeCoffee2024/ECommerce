using ECommerce.Application.CommandQueries.Security;
using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Application.Abstractions
{
    public interface ITokenService
    {
        #region Public Methods

        TokenResponse GenerateToken(User user);

        TokenResponse RefreshToken(string refreshToken, CancellationToken cancellationToken);

        #endregion Public Methods
    }
}
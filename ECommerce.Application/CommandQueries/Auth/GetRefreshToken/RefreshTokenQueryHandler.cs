using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Security;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Auth.GetRefreshToken
{
    public class RefreshTokenQueryHandler : ICommandHandler<RefreshTokenQuery>
    {
        #region Fields

        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public RefreshTokenQueryHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return Result.Failure<TokenResponse>(UserErrors.RefreshTokenRequired);
            }

            var response = _tokenService.RefreshToken(request.RefreshToken, cancellationToken);
            return Result.Success(response);
        }

        #endregion Public Methods
    }
}
using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Security;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Auth.Login
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, TokenResponse>
    {
        #region Fields

        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordService;
        private readonly LoginCommandValidator _validator;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public LoginQueryHandler(ITokenService tokenService, IUserRepository userRepository, IPasswordHasherService passwordService)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _validator = new LoginCommandValidator(userRepository, passwordService);
            _passwordService = passwordService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<TokenResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<TokenResponse>(Error.Validation, validation.Errors);

            var user = _userRepository.FindByUsername(request.UsernameEmail);
            var userEmail = _userRepository.FindByEmail(request.UsernameEmail);
            if (user != null)
            {
                if (_passwordService.VerifyPassword(user.Password, request.Password))
                {
                    var token = _tokenService.GenerateToken(user);
                    return Result.Success<TokenResponse>(token);
                }
            }
            else
            {
                if (_passwordService.VerifyPassword(userEmail.Password, request.Password))
                {
                    var token = _tokenService.GenerateToken(userEmail);
                    return Result.Success<TokenResponse>(token);
                }
            }
            validation.AddError("password", "Invalid Password");
            return Result.Failure<TokenResponse>(Error.Validation, validation.Errors);
        }

        #endregion Public Methods
    }
}
using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Validation;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Auth.Login
{
    public class LoginCommandValidator : Validator<LoginQuery>
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordService;

        #endregion Fields

        #region Public Constructors

        public LoginCommandValidator(IUserRepository userRepository, IPasswordHasherService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        #endregion Public Constructors

        #region Public Methods

        public override ValidationResult Validate(LoginQuery input)
        {
            _result
                .Required(nameof(input.UsernameEmail), input.UsernameEmail)
                .Required(nameof(input.Password), input.Password);

            var user = _userRepository.FindByUsername(input.UsernameEmail);
            if (user == null)
            {
                _result.Null(nameof(user));
                return _result;
            }
            _result
                .Exists(nameof(input.UsernameEmail), user, "User not found.")
                .Ensure(nameof(input.Password), user != null && _passwordService.VerifyPassword(user.Password, input.Password), "Incorrect password.");

            return _result;
        }

        #endregion Public Methods
    }
}
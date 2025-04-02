using ECommerce.Application.Abstractions.Validation;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.UpdateUser
{
    public class UpdateUserCommandValidator : Validator<UpdateUserCommand>
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion Fields

        #region Public Constructors

        public UpdateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public override ValidationResult Validate(UpdateUserCommand input)
        {
            _result
                .Required("Last Name", input.LastName);
            _result
                .Required("First Name", input.FirstName);
            _result
                .Required(nameof(input.Email), input.Email);
            _result
                .Required("User Name", input.UserName);
            _result
                .Required("Birth Date", input.BirthDate.ToString());

            var usernameExists = _userRepository.FindByUsername(input.UserName);
            if (usernameExists != null)
            {
                if (usernameExists!.isSuperAdmin())
                {
                    _result
                        .AddError("Super admin", "Super admin update restricted");
                }
                if (usernameExists.Id != input.Id)
                    _result
                        .Exists(nameof(input.UserName), null, "Username already exists");
            }
            var emailExists = _userRepository.FindByEmail(input.Email);
            if (emailExists != null)
            {
                if (emailExists.Id != input.Id)
                    _result
                        .Exists(nameof(input.Email), null, "Email already exists");
            }
            if (input.UserPermissions.Split(",").Length == 0 || input.UserPermissions == "")
            {
                _result
                    .LengthOutOfRange(nameof(input.UserPermissions), emailExists, 1, 0, null, "User Permissions");
            }
            return _result;
        }

        #endregion Public Methods
    }
}
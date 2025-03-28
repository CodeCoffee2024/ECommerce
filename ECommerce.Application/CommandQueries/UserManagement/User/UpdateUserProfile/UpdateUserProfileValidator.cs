using ECommerce.Application.Abstractions.Validation;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.UpdateUserProfile
{
    public class UpdateUserProfileValidator : Validator<UpdateUserProfileCommand>
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion Fields

        #region Public Constructors

        public UpdateUserProfileValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public override ValidationResult Validate(UpdateUserProfileCommand input)
        {
            _result
                .Required("Last Name", input.LastName);
            _result
                .Required("First Name", input.FirstName);
            _result
                .Required("User Name", input.UserName);
            _result
                .Required("Birth Date", input.BirthDate.ToString());

            var usernameExists = _userRepository.FindByUsername(input.UserName);
            if (usernameExists != null)
            {
                if (usernameExists.Id != input.Id)
                    _result
                        .Exists(nameof(input.UserName), null, "Username already exists");
            }
            return _result;
        }

        #endregion Public Methods
    }
}
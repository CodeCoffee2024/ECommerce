using ECommerce.Application.Abstractions.Validation;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.AddUserPermission
{
    public class AddUserPermissionCommandValidator : Validator<AddUserPermissionCommand>
    {
        #region Fields

        private readonly IUserPermissionRepository _userPermissionRepository;

        #endregion Fields

        #region Public Constructors

        public AddUserPermissionCommandValidator(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public override ValidationResult Validate(AddUserPermissionCommand input)
        {
            _result
                .Required(nameof(input.Name), input.Name);

            var user = _userPermissionRepository.FindByName(input.Name);
            if (user != null)
            {
                _result
                    .Exists(nameof(input.Name), null, "Name already exists");
            }

            if (input.Permissions.Split(",").Length == 0 || input.Permissions == "")
            {
                _result
                    .LengthOutOfRange(nameof(input.Permissions), user, 1);
            }
            return _result;
        }

        #endregion Public Methods
    }
}
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.UpdateUser
{
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUserRepository _userRepository;
        private readonly IUserUserPermissionRepository _userUserPermissionRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly UpdateUserCommandValidator _validator;

        #endregion Fields

        #region Public Constructors

        public UpdateUserCommandHandler(IUserRepository userRepository, IUserPermissionRepository userPermissionRepository, IUserUserPermissionRepository userUserPermissionRepository, IDbService dbService)
        {
            _userUserPermissionRepository = userUserPermissionRepository;
            _dbService = dbService;
            _userRepository = userRepository;
            _userPermissionRepository = userPermissionRepository;
            _validator = new UpdateUserCommandValidator(userRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Validate request
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);

            // Fetch permissions
            var permissions = (await _userPermissionRepository.GetAllPermissions(cancellationToken))
                .Where(it => request.UserPermissions.Contains(it.Id!.ToString()!))
                .ToList(); // ✅ Convert to List to avoid multiple enumerations
            var current = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            // Create user
            var user = current.Update(
                request.LastName, request.FirstName, request.MiddleName,
                request.BirthDate, request.Email, request.UserName, DateTime.Now, request.Id
            );
            foreach (var userUserPermission in user.UserUserPermissions!)
            {
                _userUserPermissionRepository.Remove(userUserPermission);
            }
            await _dbService.SaveChangesAsync();
            var userUserPermissions = permissions
                .Select(permission => UserUserPermission.Create(
                    Guid.Parse(user.Id.ToString()!),
                    Guid.Parse(permission.Id.ToString()!)
                )).ToList();
            user.SetUserUserPermissions(userUserPermissions);
            _userRepository.Update(user);
            await _dbService.SaveChangesAsync();

            return Result.Success("user");
        }

        #endregion Public Methods
    }
}
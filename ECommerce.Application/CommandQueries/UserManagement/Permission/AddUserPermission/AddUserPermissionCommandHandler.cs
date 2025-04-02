using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.AddUserPermission
{
    internal sealed class AddUserPermissionCommandHandler : ICommandHandler<AddUserPermissionCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly AddUserPermissionCommandValidator _validator;

        #endregion Fields

        #region Public Constructors

        public AddUserPermissionCommandHandler(
            IUserPermissionRepository userPermissionRepository,
            IUserRepository userRepository,
            IDbService dbService,
            IActivityLogService activityLogService
        )
        {
            _userRepository = userRepository;
            _activityLogService = activityLogService;
            _dbService = dbService;
            _userPermissionRepository = userPermissionRepository;
            _validator = new AddUserPermissionCommandValidator(userPermissionRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(AddUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);
            var userPermission = UserPermission.Create(request.Permissions, request.Name, request.Id, DateTime.Now);
            _userPermissionRepository.Add(userPermission);
            var current = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            var newValues = userPermission!.GetActivityLog(current!.FirstName + " " + current.LastName, current.FirstName + " " + current.LastName);
            await _activityLogService.LogAsync("User Permission", userPermission.Id!.Value, "New", new Dictionary<string, string>(), newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(userPermission);
        }

        #endregion Public Methods
    }
}
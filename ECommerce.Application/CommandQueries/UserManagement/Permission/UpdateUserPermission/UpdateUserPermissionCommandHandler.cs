using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.UserManagement.Permission.GetOneUserPermission;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.UpdateUserPermission
{
    internal sealed class UpdateUserPermissionCommandHandler : ICommandHandler<UpdateUserPermissionCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IPermissionService _permissionService;
        private readonly IActivityLogService _activityLogService;
        private readonly UpdateUserPermissionCommandValidator _validator;

        #endregion Fields

        #region Public Constructors

        public UpdateUserPermissionCommandHandler(
            IUserPermissionRepository userPermissionRepository,
            IDbService dbService,
            IPermissionService permissionService,
            IActivityLogService activityLogService
        )
        {
            _dbService = dbService;
            _activityLogService = activityLogService;
            _userPermissionRepository = userPermissionRepository;
            _permissionService = permissionService;
            _validator = new UpdateUserPermissionCommandValidator(userPermissionRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);
            var current = await _userPermissionRepository.GetByIdAsync(request.Id, cancellationToken);
            var oldValues = current!.GetActivityLog();
            var userPermission = current.Update(request.Permissions, request.Name, request.UpdatedById, DateTime.UtcNow);
            var oneResult = GetOneUserPermissionResponse.MapToResponse(userPermission, _permissionService.GetPermissions());
            _userPermissionRepository.Update(userPermission);
            await _dbService.SaveChangesAsync();
            var current2 = await _userPermissionRepository.GetByIdAsync(request.Id, cancellationToken);
            var newValues = current2.GetActivityLog();
            await _activityLogService.LogAsync("User Permission", request.Id, "Update", oldValues, newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(oneResult);
        }

        #endregion Public Methods
    }
}
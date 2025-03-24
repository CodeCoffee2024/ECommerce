using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetOneUserPermission
{
    internal sealed class GetOneUserPermissionQueryHandler : IQueryHandler<GetOneUserPermissionQuery, GetOneUserPermissionResponse>
    {
        #region Fields

        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IPermissionService _permissionService;

        #endregion Fields

        #region Public Constructors

        public GetOneUserPermissionQueryHandler(IUserPermissionRepository userPermissionRepository, IPermissionService permissionService)
        {
            _permissionService = permissionService;
            _userPermissionRepository = userPermissionRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<GetOneUserPermissionResponse>> Handle(GetOneUserPermissionQuery request, CancellationToken cancellationToken)
        {
            var userPermission = await _userPermissionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (userPermission is null)
            {
                return Result.Failure<GetOneUserPermissionResponse>(ValidationErrors.NotFound("User Permission"));
            }
            return GetOneUserPermissionResponse.MapToResponse(userPermission, _permissionService.GetPermissions());
        }

        #endregion Public Methods
    }
}
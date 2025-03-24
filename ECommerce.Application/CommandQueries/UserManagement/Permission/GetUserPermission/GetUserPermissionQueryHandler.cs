using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermission
{
    public class GetUserPermissionQueryHandler : IQueryHandler<GetUserPermissionQuery, PagedResult<GetUserPermissionResponse>>
    {
        #region Fields

        private readonly IUserPermissionRepository _userPermissionRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetUserPermissionQueryHandler(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetUserPermissionResponse>>> Handle(GetUserPermissionQuery request, CancellationToken cancellationToken)
        {
            PagedResult<UserPermission>? userPermission = await _userPermissionRepository.GetListingPageResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return userPermission.SetPagedResultResponse(result => GetUserPermissionResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}
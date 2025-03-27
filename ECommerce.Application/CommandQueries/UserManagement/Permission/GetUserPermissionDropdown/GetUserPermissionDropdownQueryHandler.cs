using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermissionDropdown
{
    public class GetUserPermissionDropdownQueryHandler : IQueryHandler<GetUserPermissionDropdownQuery, PagedResult<GetUserPermissionDropdownResponse>>
    {
        #region Fields

        private readonly IUserPermissionRepository _userPermissionRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetUserPermissionDropdownQueryHandler(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetUserPermissionDropdownResponse>>> Handle(GetUserPermissionDropdownQuery request, CancellationToken cancellationToken)
        {
            PagedResult<UserPermission>? userPermission = await _userPermissionRepository.GetListingPageDropdownResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return userPermission.SetPagedResultResponse(result => GetUserPermissionDropdownResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}
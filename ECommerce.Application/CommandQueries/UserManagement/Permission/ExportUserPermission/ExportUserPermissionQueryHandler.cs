using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.ExportUserPermission
{
    public class ExportUserPermissionQueryHandler : IQueryHandler<ExportUserPermissionQuery, UnpagedResult<ExportUserPermissionResponse>>
    {
        #region Fields

        private readonly IUserPermissionRepository _userPermissionRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public ExportUserPermissionQueryHandler(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<UnpagedResult<ExportUserPermissionResponse>>> Handle(ExportUserPermissionQuery request, CancellationToken cancellationToken)
        {
            UnpagedResult<UserPermission>? userPermission = await _userPermissionRepository.GetListingPageResultExportAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);

            return userPermission.SetResultResponse(result => ExportUserPermissionResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}
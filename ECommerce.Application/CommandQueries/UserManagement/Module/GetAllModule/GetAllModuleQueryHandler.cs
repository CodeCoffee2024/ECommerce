using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.UserManagement.Module.GetAllModule;
using ECommerce.Domain.Abstractions;

namespace ECommerce.Application.CommandQueries.UserManagement.Module.GetAllUserPermission
{
    public class GetAllModuleQueryHandler : IQueryHandler<GetAllModuleQuery, IEnumerable<GetAllModuleResponse>>
    {
        #region Fields

        private readonly IPermissionService _permissionService;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetAllModuleQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<IEnumerable<GetAllModuleResponse>>> Handle(GetAllModuleQuery request, CancellationToken cancellationToken)
        {
            var modules = _permissionService.GetPermissions();
            IEnumerable<GetAllModuleResponse> result = new List<GetAllModuleResponse>();
            result = modules.Select(m => GetAllModuleResponse.MapToResponse(m));

            return Result.Success(result);
        }

        #endregion Public Methods
    }
}
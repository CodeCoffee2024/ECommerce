using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.DeleteUserPermission
{
    internal class DeleteUserPermissionCommandHandler : ICommandHandler<DeleteUserPermissionCommand>
    {
        #region Fields

        private readonly IDbService _dbService;
        private readonly IUserPermissionRepository _userPermissionRepository;

        #endregion Fields

        #region Public Constructors

        public DeleteUserPermissionCommandHandler(IUserPermissionRepository userPermissionRepository, IDbService dbService)
        {
            _userPermissionRepository = userPermissionRepository;
            _dbService = dbService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
        {
            UserPermission? userPermission = await _userPermissionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (userPermission == null)
            {
                return Result.Failure(ValidationErrors.NotFound(nameof(userPermission)));
            }

            _userPermissionRepository.Remove(userPermission);

            await _dbService.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        #endregion Public Methods
    }
}
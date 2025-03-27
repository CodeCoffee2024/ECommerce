using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.DeleteUser
{
    internal class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        #region Fields

        private readonly IDbService _dbService;
        private readonly IUserRepository _userRepository;

        #endregion Fields

        #region Public Constructors

        public DeleteUserCommandHandler(IUserRepository userRepository, IDbService dbService)
        {
            _userRepository = userRepository;
            _dbService = dbService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            ECommerce.Domain.Entities.UserManagement.User? user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                return Result.Failure(ValidationErrors.NotFound(nameof(user)));
            }
            if (user.isSuperAdmin())
            {
                return Result.Failure(ValidationErrors.NotFound(nameof(user)));
            }

            _userRepository.Remove(user);

            await _dbService.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        #endregion Public Methods
    }
}
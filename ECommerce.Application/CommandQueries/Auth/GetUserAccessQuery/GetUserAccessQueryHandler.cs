using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Auth.GetUserAccessQuery
{
    public class GetUserAccessQueryHandler : IQueryHandler<GetUserAccessQuery, GetUserAccessResponse>
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetUserAccessQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<GetUserAccessResponse>> Handle(GetUserAccessQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserPermission(request.UserId);
            return Result.Success<GetUserAccessResponse>(GetUserAccessResponse.MapToResponse(user!.UserUserPermissions!));
        }

        #endregion Public Methods
    }
}
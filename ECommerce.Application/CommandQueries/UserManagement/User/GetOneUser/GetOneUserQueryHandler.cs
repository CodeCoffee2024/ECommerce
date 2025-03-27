using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.GetOneUser
{
    internal sealed class GetOneUserQueryHandler : IQueryHandler<GetOneUserQuery, GetOneUserResponse>
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion Fields

        #region Public Constructors

        public GetOneUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<GetOneUserResponse>> Handle(GetOneUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user is null)
            {
                return Result.Failure<GetOneUserResponse>(ValidationErrors.NotFound("User"));
            }
            return GetOneUserResponse.MapToResponse(user);
        }

        #endregion Public Methods
    }
}
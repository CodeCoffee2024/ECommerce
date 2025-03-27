using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.GetUser
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, PagedResult<GetUserResponse>>
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetUserResponse>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            PagedResult<ECommerce.Domain.Entities.UserManagement.User>? user = await _userRepository.GetListingPageResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return user.SetPagedResultResponse(result => GetUserResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.ExportUserListing
{
    public class ExportUserQueryHandler : IQueryHandler<ExportUserQuery, UnpagedResult<ExportUserResponse>>
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public ExportUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<UnpagedResult<ExportUserResponse>>> Handle(ExportUserQuery request, CancellationToken cancellationToken)
        {
            UnpagedResult<ECommerce.Domain.Entities.UserManagement.User>? user = await _userRepository.GetListingPageResultExportAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);

            return user.SetResultResponse(result => ExportUserResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}
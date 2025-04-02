using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.UserManagement.User.UpdateUserProfile
{
    internal sealed class UpdateUserProfileCommandHandler : ICommandHandler<UpdateUserProfileCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUserRepository _userRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IFileService _fileService;
        private readonly UpdateUserProfileValidator _validator;

        #endregion Fields

        #region Public Constructors

        public UpdateUserProfileCommandHandler(
            IUserRepository userRepository,
            IDbService dbService,
            IFileService fileService,
            IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
            _dbService = dbService;
            _fileService = fileService;
            _userRepository = userRepository;
            _validator = new UpdateUserProfileValidator(userRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            // Validate request
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);

            var current = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            var oldValues = current!.GetActivityLog();
            // Create user
            var user = current.Update(
                request.LastName, request.FirstName, request.MiddleName,
                request.BirthDate, current.Email, request.UserName, DateTime.Now, request.Id
            );

            if (request.Img != null && request.Img.Length > 0)
            {
                string uniqueFileName = await _fileService.UploadImage(request.Img);
                user.UpdateImage(uniqueFileName);
            }

            _userRepository.Update(user);
            await _dbService.SaveChangesAsync();
            var current2 = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            var newValues = current2.GetActivityLog();
            await _activityLogService.LogAsync("User", request.Id, "Update Profile", oldValues, newValues);
            await _dbService.SaveChangesAsync();

            return Result.Success("user");
        }

        #endregion Public Methods
    }
}
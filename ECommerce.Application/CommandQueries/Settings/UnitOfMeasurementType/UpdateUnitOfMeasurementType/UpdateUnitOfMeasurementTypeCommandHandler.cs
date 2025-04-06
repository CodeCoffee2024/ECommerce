using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Settings.Interfaces;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateUnitOfMeasurementType
{
    internal sealed class UpdateUnitOfMeasurementTypeCommandHandler : ICommandHandler<UpdateUnitOfMeasurementTypeCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUserRepository _userRepository;
        private readonly UpdateUnitOfMeasurementTypeValidator _validator;

        #endregion Fields

        #region Public Constructors

        public UpdateUnitOfMeasurementTypeCommandHandler(
            IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository,
            IDbService dbService,
            IActivityLogService activityLogService,
            IUserRepository userRepository
        )
        {
            _activityLogService = activityLogService;
            _dbService = dbService;
            _userRepository = userRepository;
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
            _validator = new UpdateUnitOfMeasurementTypeValidator(unitOfMeasurementTypeRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(UpdateUnitOfMeasurementTypeCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);
            var unitOfMeasurementType = _unitOfMeasurementTypeRepository.GetByIdAsync(request.Id).Result;
            var oldValues = unitOfMeasurementType!.GetActivityLog();
            unitOfMeasurementType.Update(request.Name, request.HasDecimal, DateTime.Now, request.UserId);
            _unitOfMeasurementTypeRepository.Update(unitOfMeasurementType);
            var current = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            var newValues = unitOfMeasurementType!.GetActivityLog(current!.FirstName + " " + current.LastName, current.FirstName + " " + current.LastName);
            await _activityLogService.LogAsync("Unit of Measurement Type", unitOfMeasurementType.Id!.Value, "Update", oldValues, newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(unitOfMeasurementType);
        }

        #endregion Public Methods
    }
}
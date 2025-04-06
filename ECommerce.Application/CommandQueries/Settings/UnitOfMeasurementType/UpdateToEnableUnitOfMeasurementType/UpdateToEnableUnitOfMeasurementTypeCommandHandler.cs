using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Settings.Interfaces;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateToEnableUnitOfMeasurementType
{
    internal sealed class UpdateToEnableUnitOfMeasurementTypeTypeCommandHandler : ICommandHandler<UpdateToEnableUnitOfMeasurementTypeCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUserRepository _userRepository;

        #endregion Fields

        #region Public Constructors

        public UpdateToEnableUnitOfMeasurementTypeTypeCommandHandler(
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
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(UpdateToEnableUnitOfMeasurementTypeCommand request, CancellationToken cancellationToken)
        {
            var unitOfMeasurementType = _unitOfMeasurementTypeRepository.GetByIdAsync(request.Id).Result;
            var oldValues = unitOfMeasurementType!.GetActivityLog();
            if (unitOfMeasurementType.Status != Status.Disabled.GetDescription())
                return Result.Failure<Result>(Error.Concurrency);
            unitOfMeasurementType.ToggleStatus(Status.Active.GetDescription());
            _unitOfMeasurementTypeRepository.Update(unitOfMeasurementType);
            var current = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            var newValues = unitOfMeasurementType!.GetActivityLog(current!.FirstName + " " + current.LastName, current.FirstName + " " + current.LastName);
            await _activityLogService.LogAsync("Unit of Measurement Type", unitOfMeasurementType!.Id!.Value, "Update Status", oldValues, newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(unitOfMeasurementType);
        }

        #endregion Public Methods
    }
}
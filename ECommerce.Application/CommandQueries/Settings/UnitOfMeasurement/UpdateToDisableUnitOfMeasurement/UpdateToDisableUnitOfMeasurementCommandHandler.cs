using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Settings.Interfaces;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateToDisableUnitOfMeasurement
{
    internal sealed class UpdateToDisableUnitOfMeasurementCommandHandler : ICommandHandler<UpdateToDisableUnitOfMeasurementCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUserRepository _userRepository;

        #endregion Fields

        #region Public Constructors

        public UpdateToDisableUnitOfMeasurementCommandHandler(
            IUnitOfMeasurementRepository unitOfMeasurementRepository,
            IDbService dbService,
            IActivityLogService activityLogService,
            IUserRepository userRepository
        )
        {
            _activityLogService = activityLogService;
            _dbService = dbService;
            _userRepository = userRepository;
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(UpdateToDisableUnitOfMeasurementCommand request, CancellationToken cancellationToken)
        {
            var unitOfMeasurement = _unitOfMeasurementRepository.GetByIdAsync(request.Id).Result;
            List<object> values = new List<object>();
            foreach (var uom in unitOfMeasurement!.ConvertFroms)
            {
                values.Add(new
                {
                    Id = uom.Id,
                    Name = uom.ConvertTo?.Name,
                    Value = uom.Value
                });
            }
            var oldValues = unitOfMeasurement.GetActivityLog(values);
            if (unitOfMeasurement.Status != Status.Active.GetDescription())
                return Result.Failure<Result>(Error.Concurrency);
            unitOfMeasurement.ToggleStatus(Status.Disabled.GetDescription());
            _unitOfMeasurementRepository.Update(unitOfMeasurement);
            var current = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            var newValues = unitOfMeasurement!.GetActivityLog(values, current!.FirstName + " " + current.LastName, current.FirstName + " " + current.LastName);
            await _activityLogService.LogAsync("Unit of Measurement", unitOfMeasurement!.Id!.Value, "Update Status", oldValues, newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(unitOfMeasurement);
        }

        #endregion Public Methods
    }
}
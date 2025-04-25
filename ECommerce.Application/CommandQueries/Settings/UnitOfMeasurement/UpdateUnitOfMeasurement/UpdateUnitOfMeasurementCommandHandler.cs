using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Settings.Interfaces;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateUnitOfMeasurement
{
    internal sealed class UpdateUnitOfMeasurementCommandHandler : ICommandHandler<UpdateUnitOfMeasurementCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;
        private readonly IUnitOfMeasurementConversionRepository _unitOfMeasurementConversionRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUserRepository _userRepository;
        private readonly UpdateUnitOfMeasurementValidator _validator;

        #endregion Fields

        #region Public Constructors

        public UpdateUnitOfMeasurementCommandHandler(
            IUnitOfMeasurementRepository unitOfMeasurementRepository,
            IDbService dbService,
            IActivityLogService activityLogService,
            IUnitOfMeasurementConversionRepository unitOfMeasurementConversionRepository,
            IUserRepository userRepository
        )
        {
            _activityLogService = activityLogService;
            _dbService = dbService;
            _userRepository = userRepository;
            _unitOfMeasurementConversionRepository = unitOfMeasurementConversionRepository;
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
            _validator = new UpdateUnitOfMeasurementValidator(unitOfMeasurementRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(UpdateUnitOfMeasurementCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);

            var current = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            // Get the unit of measurement and its current conversions
            var unitOfMeasurement = await _unitOfMeasurementRepository.GetByIdAsync(request.Id);
            var existingConversions = unitOfMeasurement!.ConvertTos
            .Concat(unitOfMeasurement.ConvertFroms)
            .ToList();
            List<object> oldValue = new List<object>();
            List<object> newValue = new List<object>();
            foreach (var conversion in request.Conversions)
            {
                if (conversion.Id == null)
                {
                    // Forward conversion
                    var forwardValue = conversion.Value;
                    var convForward = ECommerce.Domain.Entities.Settings.UnitOfMeasurementConversion.Create(
                        request.Id,
                        conversion.UnitOfMeasurementTo!.Value,
                        forwardValue,
                        DateTime.Now,
                        current!.Id);
                    _unitOfMeasurementConversionRepository.Add(convForward);

                    // Reverse conversion
                    var reverseValue = unitOfMeasurement.UnitOfMeasurementType!.HasDecimal
                        ? 1 / forwardValue
                        : Convert.ToInt32(1 / forwardValue);

                    var convReverse = ECommerce.Domain.Entities.Settings.UnitOfMeasurementConversion.Create(
                        conversion.UnitOfMeasurementTo!.Value,
                        request.Id,
                        reverseValue,
                        DateTime.Now,
                        current.Id);
                    _unitOfMeasurementConversionRepository.Add(convReverse);

                    var convertsTo = await _unitOfMeasurementRepository.GetByIdAsync(conversion.UnitOfMeasurementTo.Value);
                    newValue.Add(new
                    {
                        Id = convForward.Id,
                        Name = convertsTo?.Name,
                        Value = convForward.Value
                    });
                }
                else
                {
                    var conv = await _unitOfMeasurementConversionRepository.GetByIdAsync(conversion.Id.Value, cancellationToken);

                    oldValue.Add(new
                    {
                        Id = conv.Id,
                        Name = conv.ConvertTo.Name ?? conv.ConvertFrom.Name,
                        Value = conv.Value
                    });
                    conv!.Update(conversion.Value);
                    _unitOfMeasurementConversionRepository.Update(conv);
                    var convertsTo = await _unitOfMeasurementRepository.GetByIdAsync(conversion.UnitOfMeasurementTo.Value);
                    newValue.Add(new
                    {
                        Id = conv.Id,
                        Name = convertsTo?.Name,
                        Value = conv.Value
                    });
                }
            }

            // Get list of conversion IDs from the request
            var requestConversionIds = request.Conversions
                .Where(c => c.Id.HasValue)
                .Select(c => c.Id!.Value)
                .ToHashSet();

            // Remove conversions not present in the request
            foreach (var existing in existingConversions)
            {
                if (!requestConversionIds.Contains(existing.Id!.Value) && (existing.ConvertFromId != request.Id || existing.ConvertToId != request.Id))
                {
                    // Remove the original conversion
                    _unitOfMeasurementConversionRepository.Remove(existing);

                    var convertsTo = await _unitOfMeasurementRepository.GetByIdAsync(existing.ConvertFromId);
                    if (existing!.ConvertFromId != request!.Id)
                    {
                        oldValue.Add(new
                        {
                            Id = convertsTo.Id,
                            Name = convertsTo?.Name,
                            Value = existing.Value
                        });
                        //newValue.Add(new { });
                    }
                    var counterpart = await _unitOfMeasurementConversionRepository.GetCounterpartById(existing.ConvertFromId, existing.ConvertToId);

                    if (counterpart != null)
                    {
                        _unitOfMeasurementConversionRepository.Remove(counterpart);
                    }
                }
            }
            var oldValues = unitOfMeasurement!.GetActivityLog(oldValue);
            unitOfMeasurement.Update(request.Name, request.Abbreviation, DateTime.Now, request.UserId);
            _unitOfMeasurementRepository.Update(unitOfMeasurement);
            await _dbService.SaveChangesAsync();
            var currentUom = _unitOfMeasurementRepository.FindByName(unitOfMeasurement.Name);
            var newValues = currentUom!.GetActivityLog(newValue, current!.FirstName + " " + current.LastName, current.FirstName + " " + current.LastName);
            await _activityLogService.LogAsync("Unit of Measurement", unitOfMeasurement.Id!.Value, "Update", oldValues, newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(unitOfMeasurement);
        }

        #endregion Public Methods
    }
}
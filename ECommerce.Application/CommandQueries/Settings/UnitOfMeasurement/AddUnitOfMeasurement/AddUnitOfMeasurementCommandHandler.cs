using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Settings.Interfaces;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.AddUnitOfMeasurement
{
    internal sealed class AddUnitOfMeasurementCommandHandler : ICommandHandler<AddUnitOfMeasurementCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUserRepository _userRepository;
        private readonly AddUnitOfMeasurementValidator _validator;

        #endregion Fields

        #region Public Constructors

        public AddUnitOfMeasurementCommandHandler(
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
            _validator = new AddUnitOfMeasurementValidator(unitOfMeasurementRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(AddUnitOfMeasurementCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);
            var unitOfMeasurement = ECommerce.Domain.Entities.Settings.UnitOfMeasurement.Create(request.Name, request.Abbreviation, request.UnitOfMeasurementTypeId, DateTime.Now, request.Id);
            _unitOfMeasurementRepository.Add(unitOfMeasurement);
            await _dbService.SaveChangesAsync();
            var current = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            var currentUom = _unitOfMeasurementRepository.FindByName(unitOfMeasurement.Name);
            var newValues = currentUom!.GetActivityLog(current!.FirstName + " " + current.LastName, current.FirstName + " " + current.LastName);
            await _activityLogService.LogAsync("Unit of Measurement", unitOfMeasurement.Id!.Value, "New", new Dictionary<string, string>(), newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(unitOfMeasurement);
        }

        #endregion Public Methods
    }
}
using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Settings.Interfaces;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.AddUnitOfMeasurementType
{
    internal sealed class AddUnitOfMeasurementTypeCommandHandler : ICommandHandler<AddUnitOfMeasurementTypeCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUserRepository _userRepository;
        private readonly AddUnitOfMeasurementTypeValidator _validator;

        #endregion Fields

        #region Public Constructors

        public AddUnitOfMeasurementTypeCommandHandler(
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
            _validator = new AddUnitOfMeasurementTypeValidator(unitOfMeasurementTypeRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(AddUnitOfMeasurementTypeCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);
            var unitOfMeasurementType = ECommerce.Domain.Entities.Settings.UnitOfMeasurementType.Create(request.Name, request.HasDecimal, DateTime.Now, request.Id);
            _unitOfMeasurementTypeRepository.Add(unitOfMeasurementType);
            var current = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            var newValues = unitOfMeasurementType!.GetActivityLog(current!.FirstName + " " + current.LastName, current.FirstName + " " + current.LastName);
            await _activityLogService.LogAsync("Unit of Measurement Type", unitOfMeasurementType.Id!.Value, "New", new Dictionary<string, string>(), newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(unitOfMeasurementType);
        }

        #endregion Public Methods
    }
}
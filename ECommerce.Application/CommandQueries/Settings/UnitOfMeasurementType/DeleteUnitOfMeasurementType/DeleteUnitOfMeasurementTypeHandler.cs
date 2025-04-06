using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.DeleteUnitOfMeasurementType
{
    internal class DeleteUnitOfMeasurementTypeHandler : ICommandHandler<DeleteUnitOfMeasurementTypeCommand>
    {
        #region Fields

        private readonly IDbService _dbService;
        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;

        #endregion Fields

        #region Public Constructors

        public DeleteUnitOfMeasurementTypeHandler(IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository, IDbService dbService)
        {
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
            _dbService = dbService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(DeleteUnitOfMeasurementTypeCommand request, CancellationToken cancellationToken)
        {
            var unitOfMeasurementType = await _unitOfMeasurementTypeRepository!.GetByIdAsync(request!.Id, cancellationToken);

            if (_unitOfMeasurementTypeRepository == null)
            {
                return Result.Failure(ValidationErrors.NotFound(nameof(unitOfMeasurementType)));
            }

            _unitOfMeasurementTypeRepository.Remove(unitOfMeasurementType);

            await _dbService.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        #endregion Public Methods
    }
}
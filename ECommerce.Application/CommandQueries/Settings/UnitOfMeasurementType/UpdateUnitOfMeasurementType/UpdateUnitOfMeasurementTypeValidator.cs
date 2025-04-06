using ECommerce.Application.Abstractions.Validation;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateUnitOfMeasurementType
{
    public class UpdateUnitOfMeasurementTypeValidator : Validator<UpdateUnitOfMeasurementTypeCommand>
    {
        #region Fields

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;

        #endregion Fields

        #region Public Constructors

        public UpdateUnitOfMeasurementTypeValidator(IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository)
        {
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public override ValidationResult Validate(UpdateUnitOfMeasurementTypeCommand input)
        {
            _result
                .Required(nameof(input.Name), input.Name);
            _result
                .RequiredBoolean("Has Decimal", input.HasDecimal);

            var unitOfMeasurementType = _unitOfMeasurementTypeRepository.FindByName(input.Name);
            if (unitOfMeasurementType != null)
            {
                if (unitOfMeasurementType.Id != input.Id)
                {
                    _result
                        .Exists(nameof(input.Name), null, "Name already exists");
                }
            }

            return _result;
        }

        #endregion Public Methods
    }
}
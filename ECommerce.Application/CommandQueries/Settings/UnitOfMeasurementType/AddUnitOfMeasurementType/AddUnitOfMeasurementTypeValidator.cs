using ECommerce.Application.Abstractions.Validation;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.AddUnitOfMeasurementType
{
    public class AddUnitOfMeasurementTypeValidator : Validator<AddUnitOfMeasurementTypeCommand>
    {
        #region Fields

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;

        #endregion Fields

        #region Public Constructors

        public AddUnitOfMeasurementTypeValidator(IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository)
        {
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public override ValidationResult Validate(AddUnitOfMeasurementTypeCommand input)
        {
            _result
                .Required(nameof(input.Name), input.Name);
            _result
                .RequiredBoolean("Has Decimal", input.HasDecimal);

            var user = _unitOfMeasurementTypeRepository.FindByName(input.Name);
            if (user != null)
            {
                _result
                    .Exists(nameof(input.Name), null, "Name already exists");
            }

            return _result;
        }

        #endregion Public Methods
    }
}
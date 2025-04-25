using ECommerce.Application.Abstractions.Validation;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateUnitOfMeasurement
{
    public class UpdateUnitOfMeasurementValidator : Validator<UpdateUnitOfMeasurementCommand>
    {
        #region Fields

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;

        #endregion Fields

        #region Public Constructors

        public UpdateUnitOfMeasurementValidator(IUnitOfMeasurementRepository unitOfMeasurementRepository)
        {
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public override ValidationResult Validate(UpdateUnitOfMeasurementCommand input)
        {
            _result
                .Required(nameof(input.Name), input.Name);
            _result
                .Required(nameof(input.Abbreviation), input.Abbreviation);
            if (input.Abbreviation.Length > 2)
            {
                _result.LengthOutOfRange(nameof(input.Abbreviation), input.Abbreviation, 1, 2);
            }
            //_result
            //    .Required("Type", input.UnitOfMeasurementTypeId.ToString());

            var uom = _unitOfMeasurementRepository.FindByName(input.Name);
            var uomAbbreviation = _unitOfMeasurementRepository.FindByAbbreviation(input.Abbreviation);
            if (uom != null)
            {
                if (uom.Id != input.Id)
                {
                    _result
                        .Exists(nameof(input.Name), null, "Name already exists");
                }
            }
            if (uomAbbreviation != null)
            {
                if (uomAbbreviation.Id != input.Id)
                {
                    _result
                    .Exists(nameof(input.Abbreviation), null, "Abbreviation already exists");
                }
            }

            return _result;

            #endregion Public Methods
        }
    }
}
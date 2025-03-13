using ECommerce.Application.Common;

namespace ECommerce.Application.Abstractions.Validation
{
    public abstract class Validator<T>
    {
        #region Fields

        protected readonly ValidationResult _result = new();

        #endregion Fields

        #region Public Methods

        public abstract ValidationResult Validate(T input);

        #endregion Public Methods

        #region Protected Methods

        protected ValidationResult Valid() => ValidationResult.Success();

        #endregion Protected Methods
    }
}
namespace ECommerce.Domain.Abstractions
{
    public sealed class ValidatorException
    {
        #region Private Constructors

        public ValidatorException(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        #endregion Private Constructors

        #region Properties

        public string PropertyName { get; private set; } = string.Empty;
        public string ErrorMessage { get; private set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        public static List<ValidatorException> MapValidator(List<object> list)
        {
            var errors = new List<ValidatorException>();

            foreach (var item in list)
            {
                var propertyName = item.GetType().GetProperty("PropertyName")?.GetValue(item)?.ToString() ?? string.Empty;
                var errorMessage = item.GetType().GetProperty("ErrorMessage")?.GetValue(item)?.ToString() ?? string.Empty;

                errors.Add(new ValidatorException(propertyName, errorMessage));
            }

            return errors;
        }

        #endregion Public Methods
    }
}
using ECommerce.Domain.Abstractions;

namespace ECommerce.Application.Common
{
    public class ValidationResult
    {
        #region Fields

        private readonly List<Error> _errors = new();

        #endregion Fields

        #region Properties

        public List<Error> Errors => _errors;
        public bool IsValid => _errors.Count == 0;

        #endregion Properties

        #region Public Methods

        public static ValidationResult Success() => new();

        // ✅ Add an error
        public void AddError(string field, string error)
        {
            _errors.Add(new Error(field, error));
        }

        // ✅ Required Validation
        public ValidationResult Required(string field, string? value, string? message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                AddError(field, message ?? $"{field} is required.");
            return this;
        }

        public ValidationResult Null(string entity)
        {
            AddError(entity, $"{entity} not found.");
            return this;
        }

        // ✅ Exists Validation
        public ValidationResult Exists(string field, object? value, string? message = null)
        {
            if (value == null)
                AddError(field, message ?? $"{field} does not exist.");
            return this;
        }

        // ✅ Custom Validation
        public ValidationResult Ensure(string field, bool condition, string message)
        {
            if (!condition) AddError(field, message);
            return this;
        }

        #endregion Public Methods
    }
}
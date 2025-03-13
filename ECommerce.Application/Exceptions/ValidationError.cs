namespace ECommerce.Application.Exceptions
{
    public class ValidationError
    {
        #region Fields

        private readonly Dictionary<string, List<string>> _errors = new();

        #endregion Fields

        #region Properties

        public IReadOnlyDictionary<string, string[]> Errors =>
            _errors.ToDictionary(k => k.Key, v => v.Value.ToArray());

        // ✅ Check if errors exist
        public bool HasErrors => _errors.Any();

        #endregion Properties

        #region Public Methods

        // ✅ Add a new field error
        public void Add(string field, string error)
        {
            if (!_errors.ContainsKey(field))
                _errors[field] = new List<string>();

            _errors[field].Add(error);
        }

        // ✅ Required Field Validation
        public ValidationError Required(string field, string? value, string? message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                Add(field, message ?? $"{field} is required.");
            return this;
        }

        // ✅ Exists Validation (for entity checks)
        public ValidationError Exists<T>(string field, T? value, string? message = null) where T : class
        {
            if (value == null)
                Add(field, message ?? $"{field} does not exist.");
            return this;
        }

        // ✅ Min Length Validation
        public ValidationError MinLength(string field, string? value, int length, string? message = null)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < length)
                Add(field, message ?? $"{field} must be at least {length} characters.");
            return this;
        }

        // ✅ Max Length Validation
        public ValidationError MaxLength(string field, string? value, int length, string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > length)
                Add(field, message ?? $"{field} cannot exceed {length} characters.");
            return this;
        }

        // ✅ Range Validation (for numbers)
        public ValidationError Range(string field, int value, int min, int max, string? message = null)
        {
            if (value < min || value > max)
                Add(field, message ?? $"{field} must be between {min} and {max}.");
            return this;
        }

        // ✅ Custom Validation
        public ValidationError Custom(string field, bool condition, string message)
        {
            if (condition) Add(field, message);
            return this;
        }

        #endregion Public Methods
    }
}
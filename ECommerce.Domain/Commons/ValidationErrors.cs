using ECommerce.Domain.Abstractions;

namespace ECommerce.Domain.Commons
{
    public static class ValidationErrors
    {
        #region Public Methods

        public static Error Duplicate(string entityName, string propertyName) => new(
            $"{entityName}.Duplicate",
            $"{propertyName} already exists.");

        public static Error Overlap(string entityName) => new(
          $"{entityName}.Overlap",
          $"The current {entityName} is overlapping with an existing one");

        public static Error NotFound(string entityName) => new(
            $"{entityName}.NotFound",
            $"The {entityName.ToLower()} with the specified ID was not found");

        public static Error SuperAdminRestrict(string entityName) => new(
            $"{entityName}.SuperAdminRestrict",
            $"The {entityName.ToLower()} cannot perform this action");

        public static Error Required(string entityName, string propertyName) => new(
            $"{entityName}.Required",
            $"{propertyName.ToLower()} is required");

        public static Error MaxDate(string entityName, string propertyName, DateTime maxDate) => new(
            $"{entityName}.MaxDate",
            $"{propertyName} cannot be later than {maxDate:MMMM d, yyyy}.");

        public static Error Exception(string statusCode, string message) => new(
            statusCode,
            message);

        #endregion Public Methods
    }
}
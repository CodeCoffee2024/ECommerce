namespace ECommerce.Domain.Abstractions
{
    public record Error(string Name, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");
        public static readonly Error Validation = new("ValidationException", "One or more values has failed");
    }
}
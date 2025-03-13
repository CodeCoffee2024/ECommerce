namespace ECommerce.Domain.Abstractions
{
    public class DomainException(Error error) : Exception(error.Name)
    {
        #region Properties

        public Error Error { get; } = error;

        #endregion Properties
    }
}
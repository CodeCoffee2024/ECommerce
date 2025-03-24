namespace ECommerce.Domain.Abstractions
{
    public interface IDbService
    {
        #region Public Methods

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Public Methods
    }
}
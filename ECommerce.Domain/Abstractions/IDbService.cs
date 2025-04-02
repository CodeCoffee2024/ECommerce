using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Domain.Abstractions
{
    public interface IDbService
    {
        #region Public Methods

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry Entry(object entity); // Add this line

        #endregion Public Methods
    }
}
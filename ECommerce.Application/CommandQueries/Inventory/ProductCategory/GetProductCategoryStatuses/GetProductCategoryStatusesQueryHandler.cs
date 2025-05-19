using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategoryStatuses
{
    internal sealed class GetProductCategoryStatusesQueryHandler : IQueryHandler<GetProductCategoryStatusesQuery, IReadOnlyList<object>>
    {
        #region Public Methods

        public async Task<Result<IReadOnlyList<object>>> Handle(GetProductCategoryStatusesQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(ECommerce.Domain.Entities.Inventory.ProductCategory.GetStatuses());
        }

        #endregion Public Methods
    }
}
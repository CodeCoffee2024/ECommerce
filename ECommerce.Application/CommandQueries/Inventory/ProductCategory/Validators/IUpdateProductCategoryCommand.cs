namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.Validators
{
    public interface IUpdateProductCategoryCommand
    {
        #region Properties

        Guid Id { get; }
        Guid? ParentProductCategoryId { get; }
        string Name { get; }

        #endregion Properties
    }
}
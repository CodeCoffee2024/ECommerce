namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.Validators
{
    public interface IAddProductCategoryCommand
    {
        #region Properties

        Guid Id { get; }
        Guid? ParentProductCategoryId { get; }
        string Name { get; }

        #endregion Properties
    }
}
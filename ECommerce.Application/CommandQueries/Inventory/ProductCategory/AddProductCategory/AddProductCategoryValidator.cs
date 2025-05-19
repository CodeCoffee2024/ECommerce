using ECommerce.Application.Abstractions.Validation;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Inventory.Interfaces;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.AddProductCategory
{
    public class AddProductCategoryValidator : Validator<AddProductCategoryCommand>
    {
        #region Fields

        private readonly IProductCategoryRepository _productCategoryRepository;

        #endregion Fields

        #region Public Constructors

        public AddProductCategoryValidator(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public override ValidationResult Validate(AddProductCategoryCommand input)
        {
            _result
                .Required(nameof(input.Name), input.Name);
            _result
                .RequiredIf(nameof(input.ParentProductCategoryId), input.ParentProductCategoryId != null ? input.ParentProductCategoryId!.Value.ToString() : "", input.IsSubCategory!.Value ? "true" : "");

            var user = _productCategoryRepository.FindByName(input.Name);
            if (user != null)
            {
                _result
                    .Exists(nameof(input.Name), null, "Name already exists");
            }

            return _result;
        }

        #endregion Public Methods
    }
}
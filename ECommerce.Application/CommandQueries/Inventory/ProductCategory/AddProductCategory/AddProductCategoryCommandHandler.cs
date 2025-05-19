using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Inventory.Interfaces;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.AddProductCategory
{
    internal sealed class AddProductCategoryCommandHandler : ICommandHandler<AddProductCategoryCommand>
    {
        #region Fields

        private readonly IDbService _dbService;

        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IUserRepository _userRepository;
        private readonly AddProductCategoryValidator _validator;

        #endregion Fields

        #region Public Constructors

        public AddProductCategoryCommandHandler(
            IProductCategoryRepository productCategoryRepository,
            IDbService dbService,
            IActivityLogService activityLogService,
            IUserRepository userRepository
        )
        {
            _activityLogService = activityLogService;
            _dbService = dbService;
            _userRepository = userRepository;
            _productCategoryRepository = productCategoryRepository;
            _validator = new AddProductCategoryValidator(productCategoryRepository);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result> Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result.Failure<Result>(Error.Validation, validation.Errors);
            var productCategory = ECommerce.Domain.Entities.Inventory.ProductCategory.Create(request.Name, request.ParentProductCategoryId, request.IsSubCategory, DateTime.Now, request.Id);
            _productCategoryRepository.Add(productCategory);
            var current = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            var newValues = productCategory!.GetActivityLog(current!.FirstName + " " + current.LastName, current.FirstName + " " + current.LastName);
            await _activityLogService.LogAsync("Product Category", productCategory.Id!.Value, "New", new Dictionary<string, string>(), newValues);
            await _dbService.SaveChangesAsync();
            return Result.Success(productCategory);
        }

        #endregion Public Methods
    }
}
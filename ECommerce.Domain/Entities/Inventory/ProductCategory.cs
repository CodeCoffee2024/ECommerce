using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Inventory
{
    public class ProductCategory : AuditableEntity
    {
        #region Properties

        public static readonly Status[] AllowedStatuses = { Enums.Status.Active, Enums.Status.Disabled };
        public string Name { get; private set; } = string.Empty;
        public string Status { get; private set; } = string.Empty;
        public bool? IsSubCategory { get; private set; }
        public Guid? ParentProductCategoryId { get; set; }
        public virtual ProductCategory? ParentProductCategory { get; set; }
        public virtual ICollection<ProductCategory> Subcategories { get; set; } = new List<ProductCategory>();

        #endregion Properties

        #region Private Constructors

        public ProductCategory()
        { }

        private ProductCategory(string name, Guid? parentProductCategoryId, bool? isSubCategory, string status)
        {
            ParentProductCategoryId = parentProductCategoryId;
            IsSubCategory = isSubCategory;
            Status = status;
            Name = name;
        }

        public ProductCategory Update(string name, Guid? parentProductCategoryId, bool? isSubCategory, DateTime updatedDate, Guid updatedById)
        {
            SetUpdated(updatedDate, updatedById);
            Name = name;
            IsSubCategory = isSubCategory;
            ParentProductCategoryId = parentProductCategoryId;
            return this;
        }

        public ProductCategory ToggleStatus(string status)
        {
            Status = status;
            return this;
        }

        #endregion Private Constructors

        #region Private Methods

        private static readonly IReadOnlyList<object> _cachedStatuses = AllowedStatuses
        .Select(s => new { key = s.GetDescription(), value = s.GetDescription() })
        .ToList();

        public static ProductCategory Create(string name, Guid? parentProductCategoryId, bool? isSubCategory, DateTime createdDate, Guid createdById)
        {
            var uom = new ProductCategory(name, parentProductCategoryId, isSubCategory, Enums.Status.Active.GetDescription());
            uom.SetCreated(createdDate, createdById);
            return uom;
        }

        public static IReadOnlyList<object> GetStatuses() => _cachedStatuses;

        public Dictionary<string, string> GetActivityLog(string modifiedBy = "", string createdBy = "")
        {
            return new Dictionary<string, string>
            {
                { "Name", Name },
                { "Parent Product Category", ParentProductCategory == null ? "" : ParentProductCategory!.Name },
                { "Status", Status },
                { "Modified By", !string.IsNullOrEmpty(modifiedBy) ? modifiedBy : ModifiedBy?.FirstName + " " + ModifiedBy?.LastName },
                { "Created By",!string.IsNullOrEmpty(createdBy) ? createdBy : CreatedBy?.FirstName + " " + CreatedBy?.LastName }
            };
        }

        #endregion Private Methods
    }
}
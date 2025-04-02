using ECommerce.Domain.Entities.UserManagement;
using System.Text.Json.Serialization;

namespace ECommerce.Domain.Abstractions
{
    public abstract class AuditableEntity : BaseEntity
    {
        #region Properties

        public DateTime? ModifiedDate { get; set; }
        [JsonIgnore]
        public virtual User? CreatedBy { get; set; }
        [JsonIgnore]
        public virtual User? ModifiedBy { get; set; }
        public Guid? CreatedById { get; private set; }
        public Guid? ModifiedById { get; private set; }
        public DateTime? CreatedDate { get; private set; }

        #endregion Properties

        #region Private Constructors

        public void SetUpdated(DateTime? modifiedDate, Guid? modifiedById)
        {
            ModifiedById = modifiedById;
            ModifiedDate = modifiedDate;
        }

        public void SetCreated(DateTime? createdDate, Guid? createdById)
        {
            CreatedById = createdById;
            CreatedDate = createdDate;
            ModifiedById = createdById;
            ModifiedDate = createdDate;
        }

        #endregion Private Constructors
    }
}
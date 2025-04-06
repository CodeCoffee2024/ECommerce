using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Settings
{
    public class UnitOfMeasurement : AuditableEntity
    {
        #region Properties

        public virtual UnitOfMeasurementType? UnitOfMeasurementType { get; set; }
        public Guid UnitOfMeasurementTypeId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Status { get; private set; } = Enums.Status.Active.GetDescription();

        #endregion Properties

        #region Private Constructors

        public UnitOfMeasurement()
        { }

        private UnitOfMeasurement(string name, Guid unitOfMeasurementTypeId)
        {
            Name = name;
            UnitOfMeasurementTypeId = unitOfMeasurementTypeId;
        }

        public UnitOfMeasurement ToggleStatus(string status)
        {
            Status = status;
            return this;
        }

        public UnitOfMeasurement Update(string name, bool hasDecimal, Guid unitOfMeasurementTypeId)
        {
            Name = name;
            UnitOfMeasurementTypeId = unitOfMeasurementTypeId;
            return this;
        }

        #endregion Private Constructors

        #region Private Methods

        public static UnitOfMeasurement Create(string name, Guid unitOfMeasurementTypeId, DateTime? createdDate, Guid? createdById)
        {
            var uom = new UnitOfMeasurement(name, unitOfMeasurementTypeId);
            uom.SetCreated(createdDate, createdById);
            return uom;
        }

        public Dictionary<string, string> GetActivityLog(string modifiedBy = "", string createdBy = "")
        {
            return new Dictionary<string, string>
            {
                { "Name", Name },
                { "Type", UnitOfMeasurementType!.Name },
                { "Modified By", !string.IsNullOrEmpty(modifiedBy) ? modifiedBy : ModifiedBy?.FirstName + " " + ModifiedBy?.LastName },
                { "Created By",!string.IsNullOrEmpty(createdBy) ? createdBy : CreatedBy?.FirstName + " " + CreatedBy?.LastName }
            };
        }

        #endregion Private Methods
    }
}
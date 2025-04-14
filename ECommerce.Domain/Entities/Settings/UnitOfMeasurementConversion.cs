using ECommerce.Domain.Abstractions;

namespace ECommerce.Domain.Entities.Settings
{
    public class UnitOfMeasurementConversion : AuditableEntity
    {
        #region Properties

        public virtual UnitOfMeasurementType UnitOfMeasurementFrom { get; set; }
        public virtual UnitOfMeasurementType UnitOfMeasurementTo { get; private set; }
        public Guid UnitOfMeasurementFromId { get; private set; }
        public Guid UnitOfMeasurementToId { get; private set; }
        public decimal Value { get; private set; }

        #endregion Properties

        #region Private Constructors

        public UnitOfMeasurementConversion()
        { }

        private UnitOfMeasurementConversion(Guid unitOfMeasurementFromId, Guid unitOfMeasurementToId, decimal value)
        {
            UnitOfMeasurementFromId = unitOfMeasurementFromId;
            UnitOfMeasurementToId = unitOfMeasurementToId;
            Value = value;
        }

        public UnitOfMeasurementConversion Update(decimal value)
        {
            Value = value;
            return this;
        }

        #endregion Private Constructors

        #region Private Methods

        public static UnitOfMeasurementConversion Create(Guid unitOfMeasurementFromId, Guid unitOfMeasurementToId, decimal value, DateTime? createdDate, Guid? createdById)
        {
            var uom = new UnitOfMeasurementConversion(unitOfMeasurementFromId, unitOfMeasurementToId, value);
            uom.SetCreated(createdDate, createdById);
            return uom;
        }

        public Dictionary<string, string> GetActivityLog(string modifiedBy = "", string createdBy = "")
        {
            return new Dictionary<string, string>
            {
                { "From", UnitOfMeasurementFrom.Name },
                { "To", UnitOfMeasurementTo.Name },
                { "Value", Value.ToString() },
                { "Modified By", !string.IsNullOrEmpty(modifiedBy) ? modifiedBy : ModifiedBy?.FirstName + " " + ModifiedBy?.LastName },
                { "Created By",!string.IsNullOrEmpty(createdBy) ? createdBy : CreatedBy?.FirstName + " " + CreatedBy?.LastName }
            };
        }

        #endregion Private Methods
    }
}
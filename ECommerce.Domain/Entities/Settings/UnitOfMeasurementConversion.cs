using ECommerce.Domain.Abstractions;

namespace ECommerce.Domain.Entities.Settings
{
    public class UnitOfMeasurementConversion : AuditableEntity
    {
        #region Properties

        public Guid ConvertFromId { get; set; }
        public Guid ConvertToId { get; set; }
        public virtual UnitOfMeasurement ConvertFrom { get; set; }
        public virtual UnitOfMeasurement ConvertTo { get; set; }

        public decimal Value { get; private set; }

        #endregion Properties

        #region Private Constructors

        public UnitOfMeasurementConversion()
        { }

        private UnitOfMeasurementConversion(Guid convertFromId, Guid convertToId, decimal value)
        {
            ConvertFromId = convertFromId;
            ConvertToId = convertToId;
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

        public UnitOfMeasurementConversion Update(Guid unitOfMeasurementToId, decimal value, DateTime? updatedDate, Guid? updateById)
        {
            ConvertToId = unitOfMeasurementToId;
            Value = value;
            SetUpdated(updatedDate, updateById);
            return this;
        }

        public Dictionary<string, string> GetActivityLog(string modifiedBy = "", string createdBy = "")
        {
            return new Dictionary<string, string>
            {
                { "From", ConvertFrom!.Name },
                { "To", ConvertTo!.Name },
                { "Value", Value.ToString() },
                { "Modified By", !string.IsNullOrEmpty(modifiedBy) ? modifiedBy : ModifiedBy?.FirstName + " " + ModifiedBy?.LastName },
                { "Created By",!string.IsNullOrEmpty(createdBy) ? createdBy : CreatedBy?.FirstName + " " + CreatedBy?.LastName }
            };
        }

        #endregion Private Methods
    }
}
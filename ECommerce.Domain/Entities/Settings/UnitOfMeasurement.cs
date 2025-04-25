using System.Text.Json;
using System.Text.Json.Serialization;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Settings
{
    public class UnitOfMeasurement : AuditableEntity
    {
        #region Properties

        public static readonly Status[] AllowedStatuses = { Enums.Status.Active, Enums.Status.Disabled };
        public virtual UnitOfMeasurementType? UnitOfMeasurementType { get; set; }
        public Guid UnitOfMeasurementTypeId { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Abbreviation { get; private set; } = string.Empty;
        public string Status { get; private set; } = Enums.Status.Active.GetDescription();

        [JsonIgnore]
        public virtual ICollection<UnitOfMeasurementConversion> ConvertFroms { get; set; } = new List<UnitOfMeasurementConversion>();

        [JsonIgnore]
        public virtual ICollection<UnitOfMeasurementConversion> ConvertTos { get; set; } = new List<UnitOfMeasurementConversion>();

        #endregion Properties

        #region Private Constructors

        private static readonly IReadOnlyList<object> _cachedStatuses = AllowedStatuses
        .Select(s => new { key = s.GetDescription(), value = s.GetDescription() })
        .ToList();

        public UnitOfMeasurement()
        { }

        private UnitOfMeasurement(string name, string abbreviation, Guid unitOfMeasurementTypeId)
        {
            Name = name;
            Abbreviation = abbreviation;
            UnitOfMeasurementTypeId = unitOfMeasurementTypeId;
        }

        public static IReadOnlyList<object> GetStatuses() => _cachedStatuses;

        public UnitOfMeasurement ToggleStatus(string status)
        {
            Status = status;
            return this;
        }

        public UnitOfMeasurement Update(string name, string abbreviation, DateTime? modifiedDate, Guid? modifiedById)
        {
            Name = name;
            Abbreviation = abbreviation;
            SetUpdated(modifiedDate, modifiedById);
            return this;
        }

        #endregion Private Constructors

        #region Private Methods

        public static UnitOfMeasurement Create(string name, string abbreviation, Guid unitOfMeasurementTypeId, DateTime? createdDate, Guid? createdById)
        {
            var uom = new UnitOfMeasurement(name, abbreviation, unitOfMeasurementTypeId);
            uom.SetCreated(createdDate, createdById);
            return uom;
        }

        public Dictionary<string, string> GetActivityLog(
            List<object> conversions,
            string modifiedBy = "",
            string createdBy = "")
        {
            return new Dictionary<string, string>
            {
                { "Name", Name },
                { "Abbreviation", Abbreviation },
                { "Type", UnitOfMeasurementType?.Name ?? "N/A" },
                { "Conversions", JsonSerializer.Serialize(conversions) },
                { "Status", Status },
                { "Modified By", !string.IsNullOrEmpty(modifiedBy) ? modifiedBy : $"{ModifiedBy?.FirstName} {ModifiedBy?.LastName}" },
                { "Created By", !string.IsNullOrEmpty(createdBy) ? createdBy : $"{CreatedBy?.FirstName} {CreatedBy?.LastName}" }
            };
        }

        #endregion Private Methods
    }
}
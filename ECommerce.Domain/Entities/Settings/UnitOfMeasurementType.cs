using System.Text.Json.Serialization;

using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Settings
{
    public class UnitOfMeasurementType : AuditableEntity
    {
        #region Properties

        public static readonly Status[] AllowedStatuses = { Enums.Status.Active, Enums.Status.Disabled };

        [JsonIgnore]
        public virtual ICollection<UnitOfMeasurement> UnitOfMeasurements { get; set; } = new List<UnitOfMeasurement>();

        public string Name { get; private set; } = string.Empty;
        public bool HasDecimal { get; private set; }
        public string Status { get; private set; } = string.Empty;
        public virtual UnitOfMeasurement? UnitOfMeasurement { get; set; }

        #endregion Properties

        #region Private Constructors

        public UnitOfMeasurementType()
        { }

        private UnitOfMeasurementType(string name, bool hasDecimal, string status)
        {
            Status = status;
            HasDecimal = hasDecimal;
            Name = name;
        }

        public UnitOfMeasurementType Update(string name, bool hasDecimal, DateTime updatedDate, Guid updatedById)
        {
            SetUpdated(updatedDate, updatedById);
            HasDecimal = hasDecimal;
            Name = name;
            return this;
        }

        public UnitOfMeasurementType ToggleStatus(string status)
        {
            Status = status;
            return this;
        }

        #endregion Private Constructors

        #region Private Methods

        private static readonly IReadOnlyList<object> _cachedStatuses = AllowedStatuses
        .Select(s => new { key = s.GetDescription(), value = s.GetDescription() })
        .ToList();

        public static UnitOfMeasurementType Create(string name, bool hasDecimal, DateTime createdDate, Guid createdById)
        {
            var uom = new UnitOfMeasurementType(name, hasDecimal, Enums.Status.Active.GetDescription());
            uom.SetCreated(createdDate, createdById);
            return uom;
        }

        public static IReadOnlyList<object> GetStatuses() => _cachedStatuses;

        public Dictionary<string, string> GetActivityLog(string modifiedBy = "", string createdBy = "")
        {
            return new Dictionary<string, string>
            {
                { "Name", Name },
                { "Has Decimal?", HasDecimal ? "Yes" : "No" },
                { "Status", Status },
                { "Modified By", !string.IsNullOrEmpty(modifiedBy) ? modifiedBy : ModifiedBy?.FirstName + " " + ModifiedBy?.LastName },
                { "Created By",!string.IsNullOrEmpty(createdBy) ? createdBy : CreatedBy?.FirstName + " " + CreatedBy?.LastName }
            };
        }

        #endregion Private Methods
    }
}
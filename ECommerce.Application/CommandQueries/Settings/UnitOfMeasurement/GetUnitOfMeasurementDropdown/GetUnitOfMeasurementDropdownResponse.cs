using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurementDropdown
{
    public sealed class GetUnitOfMeasurementDropdownResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public Guid? Id { get; set; }

        #endregion Properties

        #region Internal Methods

        internal static GetUnitOfMeasurementDropdownResponse MapToResponse(ECommerce.Domain.Entities.Settings.UnitOfMeasurement unitOfMeasurement)
        {
            if (unitOfMeasurement == null)
                throw new ArgumentNullException(nameof(unitOfMeasurement));

            return new GetUnitOfMeasurementDropdownResponse()
            {
                Name = unitOfMeasurement.Name,
                Id = unitOfMeasurement.Id,
            };
        }

        #endregion Internal Methods
    }
}
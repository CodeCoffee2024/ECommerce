using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurement;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurementDropdown
{
    public class GetUnitOfMeasurementDropdownQuery : GetUnitOfMeasurementRequest, IQuery<PagedResult<GetUnitOfMeasurementDropdownResponse>>
    {
    }
}
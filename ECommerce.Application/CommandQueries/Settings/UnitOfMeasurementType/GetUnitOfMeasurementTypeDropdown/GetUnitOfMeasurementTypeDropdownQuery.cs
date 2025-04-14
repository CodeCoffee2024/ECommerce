using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementDropdown;
using ECommerce.Domain.Commons;

namespace ECommerce.Api.Controllers.Settings.UnitOfMeasurementType
{
    public class GetUnitOfMeasurementTypeDropdownQuery : GenericListingRequest, IQuery<PagedResult<GetUnitOfMeasurementTypeDropdownResponse>>
    {
    }
}
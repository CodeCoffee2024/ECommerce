using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementType
{
    public class GetUnitOfMeasurementTypeQuery : GetUnitOfMeasurementTypeRequest, IQuery<PagedResult<GetUnitOfMeasurementTypeResponse>>
    {
    }
}
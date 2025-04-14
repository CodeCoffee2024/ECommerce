using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurement
{
    public class GetUnitOfMeasurementQuery : GetUnitOfMeasurementRequest, IQuery<PagedResult<GetUnitOfMeasurementResponse>>
    {
    }
}
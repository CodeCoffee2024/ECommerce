using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.ExportUnitOfMeasurementTypeListing
{
    public class ExportUnitOfMeasurementTypeQuery : ExportUnitOfMeasurementTypeListingRequest, IQuery<UnpagedResult<ExportUnitOfMeasurementTypeResponse>>
    {
    }
}
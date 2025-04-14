using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.ExportUnitOfMeasurementListing
{
    public class ExportUnitOfMeasurementQuery : ExportUnitOfMeasurementListingRequest, IQuery<UnpagedResult<ExportUnitOfMeasurementResponse>>
    {
    }
}
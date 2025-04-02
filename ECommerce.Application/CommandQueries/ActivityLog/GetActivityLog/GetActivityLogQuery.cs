using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Common;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.ActivityLog.GetActivityLog;

namespace ECommerce.Application.CommandQueries.ActivityLog.GetActivityLog
{
    public class GetActivityLogQuery : GenericListingRequest, IQuery<PagedResult<GetActivityLogResponse>>
    {
    }
}
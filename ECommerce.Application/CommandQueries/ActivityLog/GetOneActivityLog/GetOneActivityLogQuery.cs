using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.ActivityLog.GetOneActivityLog
{
    public sealed record GetOneActivityLogQuery(Guid PrimaryKey) : IQuery<GetOneActivityLogResponse>;
}
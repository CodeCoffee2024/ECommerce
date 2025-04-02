namespace ECommerce.Api.Controllers.ActivityLog
{
    public sealed record ActivityLogRequest
    {
        public string Id { get; init; } = string.Empty;
    }
}
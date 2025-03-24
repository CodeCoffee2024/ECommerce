namespace ECommerce.Application.CommandQueries.Common
{
    public sealed record UserFragmentQueryResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
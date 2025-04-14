namespace ECommerce.Application.CommandQueries.Common.Mapping
{
    public sealed record UserFragmentResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
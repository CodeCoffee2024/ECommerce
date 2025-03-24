using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.Auth.GetUserAccessQuery
{
    public sealed record GetUserAccessQuery(
        Guid UserId
    ) : IQuery<GetUserAccessResponse>;
}
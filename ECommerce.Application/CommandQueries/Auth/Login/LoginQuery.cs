using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Security;

namespace ECommerce.Application.CommandQueries.Auth.Login
{
    public sealed record LoginQuery(
        string Username,
        string Password
    ) : IQuery<TokenResponse>;
}
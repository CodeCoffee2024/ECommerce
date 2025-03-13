using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.Auth.GetRefreshToken
{
    public sealed record RefreshTokenQuery(string RefreshToken) : ICommand;
}
using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.UserManagement.User.GetOneUser
{
    public sealed record GetOneUserQuery(Guid Id) : IQuery<GetOneUserResponse>;
}
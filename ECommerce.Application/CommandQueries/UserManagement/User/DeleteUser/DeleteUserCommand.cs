using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.UserManagement.User.DeleteUser
{
    public sealed record DeleteUserCommand(Guid Id) : ICommand;
}
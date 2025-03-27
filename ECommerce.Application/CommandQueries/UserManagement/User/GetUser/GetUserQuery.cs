using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.UserManagement.User.GetUser
{
    public class GetUserQuery : UserListingRequest, IQuery<PagedResult<GetUserResponse>>
    {
    }
}
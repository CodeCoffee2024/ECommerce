using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.UserManagement.User.ExportUserListing
{
    public class ExportUserQuery : ExportUserListingRequest, IQuery<UnpagedResult<ExportUserResponse>>
    {
    }
}
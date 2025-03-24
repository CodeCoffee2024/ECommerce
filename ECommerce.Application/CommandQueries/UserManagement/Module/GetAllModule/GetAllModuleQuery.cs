using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.UserManagement.Module.GetAllUserPermission;

namespace ECommerce.Application.CommandQueries.UserManagement.Module.GetAllModule
{
    public sealed record GetAllModuleQuery() : IQuery<IEnumerable<GetAllModuleResponse>>;
}
using ECommerce.Domain.Abstractions;
using MediatR;

namespace ECommerce.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
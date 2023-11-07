using MediatR;

namespace Sander.TestTask.Application.QueryHandlers;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{

}

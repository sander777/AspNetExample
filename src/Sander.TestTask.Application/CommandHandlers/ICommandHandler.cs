using MediatR;

using Sander.TestTask.Application.Commands;

namespace Sander.TestTask.Application.CommandHandlers;

public interface ICommandHandler<T, TResponse> : IRequestHandler<T, TResponse>
where T : ICommand<TResponse>
{

}

using Sander.TestTask.Application.Commands;
using MediatR;

namespace Sander.TestTask.Application.CommandHandlers;

public interface ICommandHandler<T, TResponse> : IRequestHandler<T, TResponse>
where T : ICommand<TResponse>
{

}

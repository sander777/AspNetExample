using MediatR;

namespace Sander.TestTask.Application.Commands;

public interface ICommand<T> : IRequest<T>
{

}

public interface ICommand : IRequest<Unit>
{

}
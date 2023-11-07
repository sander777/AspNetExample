using MediatR;

namespace Sander.TestTask.Application.Queries;

public interface IQuery<T> : IRequest<T>
{

}

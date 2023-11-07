using Sander.TestTask.Application.Queries;
using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask.Application.Queries;

public class MarketItemByIdQuery : IQuery<MarketItem>
{
    public int Id { get; set; }
}

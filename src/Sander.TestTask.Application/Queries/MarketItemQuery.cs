using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask.Application.Queries;

public record MarketItemQuery : IQuery<IReadOnlyCollection<MarketItem>>
{
    public string? Name { get; set; }

    public int? Limit { get; set; }

    public int? Offset { get; set; }
}

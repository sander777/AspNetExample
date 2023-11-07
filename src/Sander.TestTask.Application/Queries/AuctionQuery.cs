using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;

namespace Sander.TestTask.Application.Queries;

public class AuctionQuery : IQuery<IReadOnlyCollection<Auction>>
{
    public MarketStatus? MarketStatus { get; set; }

    public string? Seller { get; set; }

    public string? ItemName { get; set; }

    public SortingOption? Sorting { get; set; } = SortingOption.CreatedAt;

    public SortingOrder? Order { get; set; } = SortingOrder.Asc;

    public int? Limit { get; set; }

    public int? Offset { get; set; }
}
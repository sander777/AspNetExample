using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask.Domain.Repositories;

public interface IAuctionRepository
{
    Task<Auction?> GetByIdAsync(int id, CancellationToken ct);

    Task<IReadOnlyCollection<Auction>> Get(
        MarketStatus? status,
        string? seller,
        string? itemName,
        SortingOption? sortingOption = SortingOption.CreatedAt,
        SortingOrder? sortingOrder = SortingOrder.Asc,
        int? limit = null,
        int? offset = null,
        CancellationToken ct = default);

    Task<int> UpsertAsync(Auction auction, CancellationToken ct);
}

public enum SortingOption
{
    CreatedAt,
    Price,
}

public enum SortingOrder
{
    Asc,
    Desc
}
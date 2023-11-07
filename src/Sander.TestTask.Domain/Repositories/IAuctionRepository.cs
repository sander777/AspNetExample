using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask.Domain.Repositories;

public interface IAuctionRepository
{
    Task<Auction?> GetById(int id);

    Task<IReadOnlyCollection<Auction>> Get(
        MarketStatus? status,
        string? seller,
        string? itemName,
        SortingOption? sortingOption = SortingOption.CreatedAt,
        SortingOrder? sortingOrder = SortingOrder.Asc,
        int? limit = null,
        int? offset = null);

    Task<int> Upsert(Auction auction);
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
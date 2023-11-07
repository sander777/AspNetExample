using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask.Domain.Repositories;

public interface IMarketItemsRepository
{
    Task<IReadOnlyCollection<MarketItem>> Get(string? name = null, int? limit = null, int? offset = null);
    Task<MarketItem?> GetById(int id);
    Task<int> UpsertAsync(MarketItem marketItem);
}

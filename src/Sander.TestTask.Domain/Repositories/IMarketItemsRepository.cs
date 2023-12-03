using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask.Domain.Repositories;

public interface IMarketItemsRepository
{
    Task<IReadOnlyCollection<MarketItem>> GetAsync(string? name = null, int? limit = null, int? offset = null, CancellationToken ct = default);
    Task<MarketItem?> GetByIdAsync(int id, CancellationToken ct);
    Task<int> UpsertAsync(MarketItem marketItem, CancellationToken ct);
}

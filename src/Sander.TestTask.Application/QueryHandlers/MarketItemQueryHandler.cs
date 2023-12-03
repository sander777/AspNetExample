using Microsoft.Extensions.Caching.Memory;

using Sander.TestTask.Application.Queries;
using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;

namespace Sander.TestTask.Application.QueryHandlers;

public class MarketItemQueryHandler :
    IQueryHandler<MarketItemQuery, IReadOnlyCollection<MarketItem>>,
    IQueryHandler<MarketItemByIdQuery, MarketItem?>
{
    private readonly IMarketItemsRepository _marketItemsRepository;
    private readonly IMemoryCache _memoryCache;

    public MarketItemQueryHandler(IMarketItemsRepository marketItemsRepository, IMemoryCache memoryCache)
    {
        _marketItemsRepository = marketItemsRepository;
        _memoryCache = memoryCache;
    }

    public Task<IReadOnlyCollection<MarketItem>> Handle(
        MarketItemQuery request,
        CancellationToken cancellationToken)
    {
        var items = _marketItemsRepository.GetAsync(request.Name, request.Limit, request.Offset);
        return items;
    }

    public Task<MarketItem?> Handle(MarketItemByIdQuery request, CancellationToken ct)
    {
        var res = _memoryCache.GetOrCreateAsync<MarketItem>($"market_item_{request.Id}",
            (_) =>
            {
                return _marketItemsRepository.GetByIdAsync(request.Id, ct);
            });
        return res;
    }
}

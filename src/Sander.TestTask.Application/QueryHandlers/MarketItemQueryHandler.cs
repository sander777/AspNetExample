using Sander.TestTask.Application.Queries;
using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

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
        var items = _marketItemsRepository.Get(request.Name, request.Limit, request.Offset);
        return items;
    }

    public Task<MarketItem?> Handle(MarketItemByIdQuery request, CancellationToken cancellationToken)
    {
        var res = _memoryCache.GetOrCreateAsync<MarketItem>($"market_item_{request.Id}",
            (_) =>
            {
                return _marketItemsRepository.GetById(request.Id);
            });
        return res;
    }
}

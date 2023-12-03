using Microsoft.Extensions.Caching.Memory;

using Sander.TestTask.Application.Queries;
using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;

namespace Sander.TestTask.Application.QueryHandlers;

public class AuctionQueryHandler :
    IQueryHandler<AuctionByIdQuery, Auction?>,
    IQueryHandler<AuctionQuery, IReadOnlyCollection<Auction>>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IMemoryCache _memoryCache;

    public AuctionQueryHandler(IAuctionRepository auctionRepository, IMemoryCache memoryCache)
    {
        _auctionRepository = auctionRepository;
        _memoryCache = memoryCache;
    }

    public Task<IReadOnlyCollection<Auction>> Handle(AuctionQuery request, CancellationToken ct)
    {
        var items = _auctionRepository.Get(
            request.MarketStatus,
            request.Seller, 
            request.ItemName,
            request.Sorting,
            request.Order,
            request.Limit,
            request.Offset,
            ct);
        return items;
    }

    public Task<Auction?> Handle(AuctionByIdQuery request, CancellationToken ct)
    {
        var res = _memoryCache.GetOrCreateAsync<Auction>($"auction_{request.Id}",
            (_) =>
            {
                return _auctionRepository.GetByIdAsync(request.Id, ct);
            });
        return res;
    }
}

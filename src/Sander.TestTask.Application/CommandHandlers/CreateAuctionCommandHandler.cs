using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using Sander.TestTask.Domain;
using Sander.TestTask.Domain.Repositories;

namespace Sander.TestTask.Application.CommandHandlers;

public class CreateAuctionCommandHandler : ICommandHandler<CreateAuctionCommand, int>
{
    private readonly IMarketItemsRepository _marketItemsRepository;
    private readonly IAuctionRepository _auctionRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<CreateAuctionCommandHandler> _logger;

    public CreateAuctionCommandHandler(
        IMarketItemsRepository marketItemsRepository,
        IAuctionRepository auctionRepository,
        IMemoryCache memoryCache,
        ILogger<CreateAuctionCommandHandler> logger)
    {

        _marketItemsRepository = marketItemsRepository;
        _auctionRepository = auctionRepository;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<int> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating action for item @id", request.ItemId);
        var item = await _marketItemsRepository.GetById(request.ItemId);
        if (item is null)
        {
            throw new MarketItemNotFoundException(request.ItemId);
        }
        var auction = item.CreateAuctionForItem(request.InitialPrice, request.Seller ?? string.Empty);

        var id = await _auctionRepository.Upsert(auction);
        _memoryCache.Remove($"auction_{id}");
        _logger.LogInformation("Created action for item @id", request.ItemId);

        return id;
    }
}

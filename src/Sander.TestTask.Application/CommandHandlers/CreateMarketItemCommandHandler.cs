using Sander.TestTask.Application.Commands;
using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Sander.TestTask.Application.CommandHandlers;

public class CreateMarketItemCommandHandler : ICommandHandler<CreateMarketItemCommand, int>
{
    private readonly IMarketItemsRepository _repository;
    private readonly IMemoryCache _memoryCache;
    private ILogger _logger;

    public CreateMarketItemCommandHandler(
        IMarketItemsRepository repository,
        IMemoryCache memoryCache,
        ILogger<CreateMarketItemCommandHandler> logger)
    {
        _repository = repository;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<int> Handle(CreateMarketItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating item");
        var id = await _repository.UpsertAsync(new Domain.Entities.MarketItem
        {
            Name = request.Name,
            Description = request.Description,
            MetaData = request.MetaData
        });

        _memoryCache.Remove($"market_item_{id}");
        _logger.LogInformation("Created item with @id", id);

        return id;
    }
}

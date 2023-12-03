using Microsoft.EntityFrameworkCore;

using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;
using Sander.TestTask.Persistance.Mssql.Entities;

namespace Sander.TestTask.Persistance.Mssql.Repository;

public class MarketItemRepository : IMarketItemsRepository
{
    private readonly MarketDbContext _dbContext;

    public MarketItemRepository(MarketDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<MarketItem>> GetAsync(
        string? name = null,
        int? limit = null,
        int? offset = null,
        CancellationToken ct = default)
    {
        var query = _dbContext.MarketItems.AsQueryable();
        if (name is not null)
        {
            query = query.Where(x => EF.Functions.Contains(x.Name, $"\"*{name}*\""));

        }
        if (offset is not null)
        {
            query = query.Skip(offset.Value);

        }
        if (limit is not null)
        {
            query = query.Take(limit.Value);

        }
        var result = await query.ToListAsync(ct);

        return result
            .Select(ToDomain)
            .OfType<MarketItem>()
            .ToList();
    }

    public async Task<MarketItem?> GetByIdAsync(int id, CancellationToken ct)
    {
        return ToDomain(await _dbContext.MarketItems.FirstOrDefaultAsync(x => x.Id == id, ct));
    }

    public async Task<int> UpsertAsync(MarketItem marketItem, CancellationToken ct)
    {
        var entity = ToEntity(marketItem);
        _dbContext.MarketItems.Update(entity);
        await _dbContext.SaveChangesAsync(ct);
        return entity.Id;
    }

    internal static MarketItem? ToDomain(MarketItemEntity? entity)
        => entity is null ? null : new MarketItem
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            MetaData = entity.MetaData,
        };

    internal static MarketItemEntity ToEntity(MarketItem item)
        => new MarketItemEntity
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            MetaData = item.MetaData,
        };
}

using System.Runtime.CompilerServices;
using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;
using Sander.TestTask.Persistance.Mssql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sander.TestTask.Persistance.Mssql.Repository;

public class MarketItemRepository : IMarketItemsRepository
{
    private readonly MarketDbContext _dbContext;

    public MarketItemRepository(MarketDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<MarketItem>> Get(
        string? name = null,
        int? limit = null,
        int? offset = null)
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
        var result = await query.ToListAsync();

        return result
            .Select(ToDomain)
            .OfType<MarketItem>()
            .ToList();
    }

    public async Task<MarketItem?> GetById(int id)
    {
        return ToDomain(await _dbContext.MarketItems.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<int> UpsertAsync(MarketItem marketItem)
    {
        var entity = ToEntity(marketItem);
        _dbContext.MarketItems.Update(entity);
        await _dbContext.SaveChangesAsync();
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

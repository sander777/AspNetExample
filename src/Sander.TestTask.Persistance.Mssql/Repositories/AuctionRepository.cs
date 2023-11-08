using Microsoft.EntityFrameworkCore;

using Sander.TestTask.Domain;
using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;
using Sander.TestTask.Persistance.Mssql.Entities;
using Sander.TestTask.Persistance.Mssql.Repository;

namespace Sander.TestTask.Persistance.Mssql;

public class AuctionRepository : IAuctionRepository
{
    private readonly MarketDbContext _dbContext;

    public AuctionRepository(MarketDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Auction>> Get(
        MarketStatus? status,
        string? seller,
        string? itemName,
        SortingOption? sortingOption = SortingOption.CreatedAt,
        SortingOrder? sortingOrder = SortingOrder.Asc,
        int? limit = null,
        int? offset = null)
    {
        var query = _dbContext.Auctions.AsQueryable();
        if (status is not null)
        {
            query = query.Where(x => x.Status == status);
        }
        if (seller is not null)
        {
            query = query.Where(x => x.Seller == seller);
        }
        if (itemName is not null)
        {
            query = query
            .Include(x => x.MarketItem)
            .Where(x => EF.Functions.Contains(x.MarketItem.Name, $"\"*{itemName}*\""));
        }
        if (sortingOrder == SortingOrder.Asc)
        {
            query = sortingOption switch
            {
                SortingOption.CreatedAt => query.OrderBy(x => x.CreatedAt),
                SortingOption.Price => query.OrderBy(x => x.Price),
                _ => throw new NotImplementedException(),
            };
        }
        else
        {
            query = sortingOption switch
            {
                SortingOption.CreatedAt => query.OrderByDescending(x => x.CreatedAt),
                SortingOption.Price => query.OrderByDescending(x => x.Price),
                _ => throw new NotImplementedException(),
            };
        }
        if (offset is not null)
        {
            query = query.Skip(offset.Value);

        }
        if (limit is not null)
        {
            query = query.Take(limit.Value);

        }
        var result = await query.Include(x => x.MarketItem).ToListAsync();

        return result
            .Select(ToDomain)
            .OfType<Auction>()
            .ToList();
    }

    public Task<Auction> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Upsert(Auction auction)
    {
        var entity = new AuctionEntity
        {
            Id = 0,
            CreatedAt = auction.CreatedAt,
            MarketItem = await _dbContext.MarketItems.FirstAsync(x => x.Id == auction.Item.Id),
            Price = auction.Price,
            Seller = auction.Seller,
            Status = auction.Status,
        };

        _dbContext.Auctions.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    private static Auction? ToDomain(AuctionEntity? entity)
        => entity is null ? null : new Auction
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            Item = MarketItemRepository.ToDomain(entity.MarketItem)
                ?? throw new MarketItemNotFoundException(entity.MarketItem.Id),
            Buyer = entity.Buyer,
            FinishedAt = entity.FinishedAt,
            Price = entity.Price,
            Seller = entity.Seller,
            Status = entity.Status
        };
}

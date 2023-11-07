namespace Sander.TestTask.Domain.Entities;

public record MarketItem
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? MetaData { get; set; }

    public Auction CreateAuctionForItem(decimal initialPrice, string seller)
    {
        return new()
        {
            CreatedAt = DateTime.UtcNow,
            Item = this,
            Seller = seller,
            Status = MarketStatus.Active,
            Price = initialPrice
        };
    }
}

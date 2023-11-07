using System.Text.Json.Serialization;

namespace Sander.TestTask.Domain.Entities;

public class Auction : IAggregateRoot
{
    public int Id { get; set; }

    public required MarketItem Item { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public decimal? Price { get; set; }

    public MarketStatus Status { get; set; }

    public string? Seller { get; set; }

    public string? Buyer { get; set; }
}

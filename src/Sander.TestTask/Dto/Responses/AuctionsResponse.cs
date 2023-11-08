using System.Text.Json.Serialization;

using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask;

public class AuctionsResponse
{
    [JsonPropertyName("items")]
    public required IReadOnlyCollection<AuctionDto> Items { get; set; }

    [JsonPropertyName("page_size")]
    public int? PageSize { get; set; }

    [JsonPropertyName("next_page")]
    public int? NextPage { get; set; }
}

public class AuctionDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("item_id")]
    public required int MarketItem { get; set; }

    [JsonPropertyName("item_name")]
    public string? MarketItemName { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("finished_at")]
    public DateTime? FinishedAt { get; set; }

    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [JsonPropertyName("status")]
    public MarketStatus Status { get; set; }

    [JsonPropertyName("seller")]
    public string? Seller { get; set; }

    [JsonPropertyName("buyer")]
    public string? Buyer { get; set; }

    public static AuctionDto ToDto(Auction auction)
    => new()
    {
        Id = auction.Id,
        CreatedAt = auction.CreatedAt,
        MarketItem = auction.Item.Id,
        MarketItemName = auction.Item.Name,
        Buyer = auction.Buyer,
        FinishedAt = auction.FinishedAt,
        Price = auction.Price,
        Seller = auction.Seller,
        Status = auction.Status
    };
}

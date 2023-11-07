using System.Text.Json.Serialization;

namespace Sander.TestTask;

public class CreateAuctionRequest
{
    [JsonPropertyName("item_id")]
    public required int ItemId { get; set; }

    [JsonPropertyName("initial_price")]
    public decimal InitialPrice { get; set; }
    
    [JsonPropertyName("seller")]
    public string? Seller { get; set; }
}

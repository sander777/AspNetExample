using System.Text.Json.Serialization;

namespace Sander.TestTask.Dto.Requests;

public record CreateMarketItemRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("meta_data")]
    public string? MetaData { get; set; }
}

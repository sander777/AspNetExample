﻿using System.Text.Json.Serialization;

using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask.Dto.Responses;

public class MarketItemsResponse
{
    [JsonPropertyName("items")]
    public required IReadOnlyCollection<MarketItemDto> Items { get; set; }

    [JsonPropertyName("page_size")]
    public int? PageSize { get; set; }

    [JsonPropertyName("next_page")]
    public int? NextPage { get; set; }
}

public class MarketItemDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("meta_data")]
    public string? MetaData { get; set; }

    public static MarketItemDto ToDto(MarketItem marketItem)
        => new()
        {
            Id = marketItem.Id,
            Name = marketItem.Name,
            Description = marketItem.Description,
            MetaData = marketItem.MetaData
        };
}


using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;

namespace Sander.TestTask;

public class GetAuctionRequest
{
    [FromQuery(Name = "status")]
    public MarketStatus? MarketStatus { get; set; }

    [FromQuery(Name = "seller")]
    public string? Seller { get; set; }

    [FromQuery(Name = "name")]
    public string? ItemName { get; set; }

    [FromQuery(Name = "sorting")]
    public SortingOption? Sorting { get; set; } = SortingOption.CreatedAt;

    [FromQuery(Name = "order")]
    public SortingOrder? Order { get; set; } = SortingOrder.Asc;

    [FromQuery(Name = "page_size")]
    [Range(1, 1000)]
    public int PageSize { get; set; } = 20;

    [FromQuery(Name = "page")]
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
}

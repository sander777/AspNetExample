using Sander.TestTask.Domain.Entities;
using Sander.TestTask.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

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

    [FromQuery(Name = "limit")]
    public int? Limit { get; set; }

    [FromQuery(Name = "offset")]
    public int? Offset { get; set; }
}

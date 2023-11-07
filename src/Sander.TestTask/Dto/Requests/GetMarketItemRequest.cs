using Microsoft.AspNetCore.Mvc;

namespace Sander.TestTask;

public class GetMarketItemRequest
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "limit")]
    public int? Limit { get; set; }

    [FromQuery(Name = "offset")]
    public int? Offset { get; set; }
}

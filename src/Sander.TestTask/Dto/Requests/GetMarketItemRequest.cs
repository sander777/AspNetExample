using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace Sander.TestTask;

public class GetMarketItemRequest
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "page_size")]
    [Range(1, 1000)]
    public int PageSize { get; set; } = 20;

    [FromQuery(Name = "page")]
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
}

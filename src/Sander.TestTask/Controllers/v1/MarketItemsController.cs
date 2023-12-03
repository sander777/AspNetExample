using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;

using MediatR;

using Sander.TestTask;
using Sander.TestTask.Application.Commands;
using Sander.TestTask.Application.Queries;
using Sander.TestTask.Dto.Requests;
using Sander.TestTask.Dto.Responses;

[ApiVersion("1.0")]
[Route("api/v1/items")]
public class MarketItemsController : Controller
{
    private readonly IMediator _mediator;

    public MarketItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<MarketItemsResponse>> Get([FromQuery] GetMarketItemRequest request, CancellationToken ct)
    {
        var query = new MarketItemQuery
        {
            Name = request.Name,
            Limit = request.PageSize + 1,
            Offset = (request.Page - 1) * request.PageSize
        };
        var response = await _mediator.Send(query, ct);
        var items = response.Select(MarketItemDto.ToDto).ToList();
        return Ok(new MarketItemsResponse
        {
            Items = items.Take(request.PageSize).ToList(),
            PageSize = request.PageSize,
            NextPage = items.Count <= request.PageSize ? null : request.Page + 1,
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MarketItemDto>> GetById([FromRoute] int id, CancellationToken ct)
    {
        var query = new MarketItemByIdQuery
        {
            Id = id,
        };
        var response = await _mediator.Send(query, ct);
        if (response is null)
        {
            return NotFound();
        }
        return Ok(MarketItemDto.ToDto(response));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreateMarketItemRequest request, CancellationToken ct)
    {
        var command = new CreateMarketItemCommand
        {
            Name = request.Name,
            Description = request.Description,
            MetaData = request.MetaData
        };
        var id = await _mediator.Send(command, ct);

        return Ok(id);
    }
}
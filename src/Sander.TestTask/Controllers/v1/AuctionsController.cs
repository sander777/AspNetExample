using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;

using MediatR;

using Sander.TestTask.Application;
using Sander.TestTask.Application.Queries;
using Sander.TestTask.Domain;

namespace Sander.TestTask;

[ApiVersion("1.0")]
[Route("api/v1/auctions")]
public class AuctionsController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuctionsController> _logger;

    public AuctionsController(IMediator mediator, ILogger<AuctionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreateAuctionRequest request, CancellationToken ct)
    {
        var command = new CreateAuctionCommand
        {
            ItemId = request.ItemId,
            InitialPrice = request.InitialPrice,
            Seller = request.Seller
        };
        try
        {
            var id = await _mediator.Send(command, ct);
            return Ok(id);
        }
        catch (MarketItemNotFoundException e)
        {
            _logger.LogError(e.Message);
            return NotFound(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetById([FromRoute] int id, CancellationToken ct)
    {
        var query = new AuctionByIdQuery
        {
            Id = id,
        };
        var response = await _mediator.Send(query, ct);
        if (response is null)
        {
            return NotFound();
        }
        return Ok(AuctionDto.ToDto(response));
    }

    [HttpGet]
    public async Task<ActionResult<AuctionsResponse>> Get([FromQuery] GetAuctionRequest request, CancellationToken ct)
    {
        var query = new AuctionQuery
        {
            ItemName = request.ItemName,
            MarketStatus = request.MarketStatus,
            Order = request.Order,
            Seller = request.Seller,
            Sorting = request.Sorting,
            Limit = request.PageSize + 1,
            Offset = (request.Page - 1) * request.PageSize
        };
        var response = await _mediator.Send(query, ct);
        var items = response.Select(AuctionDto.ToDto).ToList();
        return Ok(new AuctionsResponse
        {
            Items = items.Take(request.PageSize).ToList(),
            PageSize = request.PageSize,
            NextPage = items.Count <= request.PageSize ? null : request.Page + 1,
        });
    }
}

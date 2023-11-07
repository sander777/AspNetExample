using Sander.TestTask.Application;
using Sander.TestTask.Application.Queries;
using Sander.TestTask.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sander.TestTask;

[Route("api/auctions")]
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
    public async Task<ActionResult<int>> Post([FromBody] CreateAuctionRequest request)
    {
        var command = new CreateAuctionCommand
        {
            ItemId = request.ItemId,
            InitialPrice = request.InitialPrice,
            Seller = request.Seller
        };
        try
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
        catch (MarketItemNotFoundException e)
        {
            _logger.LogError(e.Message);
            return NotFound(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetById([FromRoute] int id)
    {
        var query = new AuctionByIdQuery
        {
            Id = id,
        };
        var response = await _mediator.Send(query);
        if (response is null)
        {
            return NotFound();
        }
        return Ok(AuctionDto.ToDto(response));
    }

    [HttpGet]
    public async Task<ActionResult<AuctionsResponse>> Get([FromQuery] GetAuctionRequest request)
    {
        var query = new AuctionQuery
        {
            ItemName = request.ItemName,
            MarketStatus = request.MarketStatus,
            Order = request.Order,
            Seller = request.Seller,
            Sorting = request.Sorting,
            Limit = request.Limit,
            Offset = request.Offset
        };
        var response = await _mediator.Send(query);
        var items = response.Select(AuctionDto.ToDto).ToList();
        return Ok(new AuctionsResponse
        {
            Items = items,
            Limit = request.Limit ?? items.Count,
            Offset = (request.Offset ?? 0) + items.Count,
        });
    }
}

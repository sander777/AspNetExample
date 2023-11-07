using Sander.TestTask.Domain.Entities;

namespace Sander.TestTask.Application.Queries;

public class AuctionByIdQuery : IQuery<Auction?>
{
    public int Id { get; set; }
}


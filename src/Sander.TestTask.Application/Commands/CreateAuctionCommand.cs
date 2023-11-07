using Sander.TestTask.Application.Commands;

namespace Sander.TestTask.Application;

public class CreateAuctionCommand : ICommand<int>
{
    public required int ItemId { get; set; }

    public decimal InitialPrice { get; set; }
    
    public string? Seller { get; set; }
}

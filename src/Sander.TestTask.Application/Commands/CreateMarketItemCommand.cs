namespace Sander.TestTask.Application.Commands;

public class CreateMarketItemCommand : ICommand<int>
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? MetaData { get; set; }
}

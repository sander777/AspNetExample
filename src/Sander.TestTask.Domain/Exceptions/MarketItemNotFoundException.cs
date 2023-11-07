namespace Sander.TestTask.Domain;

public class MarketItemNotFoundException : Exception
{
    public MarketItemNotFoundException(int id)
        : base($"Con't find market item with id {id}")
    { }
}

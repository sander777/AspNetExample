using System.ComponentModel;

namespace Sander.TestTask.Domain.Entities;

public enum MarketStatus
{
    [Description("None")]
    None,
    
    [Description("Canceled")]
    Canceled,
    
    [Description("Finished")]
    Finished,
    
    [Description("Active")]
    Active
}

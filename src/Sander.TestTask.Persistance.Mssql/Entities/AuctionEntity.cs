using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sander.TestTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sander.TestTask.Persistance.Mssql.Entities;

[Table("auction")]
[Index(nameof(Seller))]
[Index(nameof(Status))]
[Index(nameof(CreatedAt))]
[Index(nameof(Price))]
public class AuctionEntity
{
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    [ForeignKey("item_id")]
    public required MarketItemEntity MarketItem { get; set; }

    [Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    [Column("finished_at")]
    public DateTime? FinishedAt { get; set; }

    [Column("price", TypeName = "money")]
    public decimal? Price { get; set; }

    [Column("status")]
    public MarketStatus Status { get; set; }

    [Column("seller")]
    public string? Seller { get; set; }

    [Column("buyer")]
    public string? Buyer { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sander.TestTask.Persistance.Mssql.Entities;

[Index(nameof(Name))]
[Table("market_items")]
public class MarketItemEntity
{
    [Key]
    [Column("id")]
    public required  int Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("meta_data")]
    public string? MetaData { get; set; }
}

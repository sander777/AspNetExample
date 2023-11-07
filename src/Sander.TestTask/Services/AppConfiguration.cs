using System.ComponentModel.DataAnnotations;
using Sander.TestTask.Persistance.Mssql;

namespace Sander.TestTask.Services;

public class AppConfiguration
{
    [Required]
    public SqlServerSettings SqlServerSettings { get; set; } = default!;
}

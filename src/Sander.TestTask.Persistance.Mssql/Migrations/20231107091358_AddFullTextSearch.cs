using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sander.TestTask.Persistance.Mssql.Migrations
{
    /// <inheritdoc />
    public partial class AddFullTextSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                CREATE FULLTEXT CATALOG items_names AS DEFAULT;
                CREATE FULLTEXT INDEX ON market_items (name) KEY INDEX PK_market_items ON items_names;",
                true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP FULLTEXT INDEX ON market_items;
                DROP FULLTEXT CATALOG items_names;",
                true);
        }
    }
}

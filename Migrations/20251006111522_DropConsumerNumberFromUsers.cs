using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPayMobileApp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class DropConsumerNumberFromUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop index if it exists before dropping the column
            migrationBuilder.Sql(@"
IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_Users_consumerNumber' AND object_id = OBJECT_ID('Users'))
BEGIN
    DROP INDEX [IX_Users_consumerNumber] ON [Users];
END");

            migrationBuilder.DropColumn(
                name: "consumerNumber",
                table: "Users");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
              name: "consumerNumber",
              table: "Users",
              type: "nvarchar(30)",
              maxLength: 30,
              nullable: true);

            // Recreate index (optional if it existed before)
            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_Users_consumerNumber' AND object_id = OBJECT_ID('Users'))
BEGIN
    CREATE INDEX [IX_Users_consumerNumber] ON [Users]([consumerNumber]);
END");
        }
    }
}

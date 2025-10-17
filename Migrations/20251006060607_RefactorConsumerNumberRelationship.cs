using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPayMobileApp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class RefactorConsumerNumberRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create ConsumerNumbers table
            migrationBuilder.CreateTable(
                name: "ConsumerNumbers",
                columns: table => new
                {
                    consumerNumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerNumbers", x => x.consumerNumberId);
                    table.ForeignKey(
                        name: "FK_ConsumerNumbers_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create index for ConsumerNumbers
            migrationBuilder.CreateIndex(
                name: "IX_ConsumerNumbers_userId_number",
                table: "ConsumerNumbers",
                columns: new[] { "userId", "number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop ConsumerNumbers table
            migrationBuilder.DropTable(
                name: "ConsumerNumbers");
        }
    }
}

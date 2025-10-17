using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPayMobileApp_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateBillsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    billId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    issueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    consumerNumberId = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.billId);
                    table.ForeignKey(
                        name: "FK_Bills_ConsumerNumbers_consumerNumberId",
                        column: x => x.consumerNumberId,
                        principalTable: "ConsumerNumbers",
                        principalColumn: "consumerNumberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_consumerNumberId",
                table: "Bills",
                column: "consumerNumberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}

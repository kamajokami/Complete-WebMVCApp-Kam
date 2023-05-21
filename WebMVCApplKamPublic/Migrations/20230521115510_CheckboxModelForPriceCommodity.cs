using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVCApplKamPublic.Migrations
{
    public partial class CheckboxModelForPriceCommodity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComodityPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    PriceMoney = table.Column<int>(type: "INTEGER", nullable: false),
                    Width = table.Column<string>(type: "TEXT", nullable: true),
                    IsSale = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCommodityActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPropertyOwner = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBuyerMan = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBuyerWoman = table.Column<bool>(type: "INTEGER", nullable: false),
                    SendConsent = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComodityPrices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComodityPrices_Title",
                table: "ComodityPrices",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComodityPrices");
        }
    }
}

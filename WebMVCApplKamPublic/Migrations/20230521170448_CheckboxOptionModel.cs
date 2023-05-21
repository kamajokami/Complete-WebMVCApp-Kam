using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVCApplKamPublic.Migrations
{
    public partial class CheckboxOptionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionCheckboxOptionId",
                table: "ComodityPrices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ComodityPrices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckBoxOptions",
                columns: table => new
                {
                    CheckboxOptionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsChecked = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBoxOptions", x => x.CheckboxOptionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComodityPrices_OptionCheckboxOptionId",
                table: "ComodityPrices",
                column: "OptionCheckboxOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComodityPrices_CheckBoxOptions_OptionCheckboxOptionId",
                table: "ComodityPrices",
                column: "OptionCheckboxOptionId",
                principalTable: "CheckBoxOptions",
                principalColumn: "CheckboxOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComodityPrices_CheckBoxOptions_OptionCheckboxOptionId",
                table: "ComodityPrices");

            migrationBuilder.DropTable(
                name: "CheckBoxOptions");

            migrationBuilder.DropIndex(
                name: "IX_ComodityPrices_OptionCheckboxOptionId",
                table: "ComodityPrices");

            migrationBuilder.DropColumn(
                name: "OptionCheckboxOptionId",
                table: "ComodityPrices");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ComodityPrices");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVCApplKamPublic.Migrations
{
    public partial class CheckboxModelForPriceCommodityAddNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PriceMoney",
                table: "ComodityPrices",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "IsCommodityActiveCheckboxDescription",
                table: "ComodityPrices",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCommodityActiveCheckboxDescription",
                table: "ComodityPrices");

            migrationBuilder.AlterColumn<int>(
                name: "PriceMoney",
                table: "ComodityPrices",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}

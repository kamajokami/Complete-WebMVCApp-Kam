using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVCApplKamPublic.Migrations
{
    public partial class UserTableModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PropertyOwner",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyOwner",
                table: "Users");
        }
    }
}
